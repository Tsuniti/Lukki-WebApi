using ErrorOr;
using Lukki.Application.Colors.Commands.CreateColor;
using Lukki.Application.Colors.Queries.GetAllColors;
using Lukki.Contracts.Colors;
using Lukki.Domain.ColorAggregate;
using Lukki.Domain.Common.Errors;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

[Route("colors")]
public class ColorsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ColorsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }


    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ColorResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateColor([FromForm] CreateColorRequest request, [FromForm] IFormFile? image)
    {
        Stream? streamImage = null;
        if (image is not null)
        {
            const int maxFileSizeBytes = 20 * 1024; // 20 KB
            if (image.Length > maxFileSizeBytes)
            {
                return Problem(
                    new List<Error>
                    {
                        Errors.Color.ImageTooLarge(
                            yourImageSize: image.Length,
                            maxImageSize: maxFileSizeBytes)
                    });
            }

            streamImage = await FileHelpers.ConvertToStreamAsync(image);
        }

        var command = _mapper.Map<CreateColorCommand>(request);


        var createColorResult = await _mediator.Send(command with { Image = streamImage });


        return createColorResult.Match(
            color => Ok(_mapper.Map<ColorResponse>(color)),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<ColorResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllColors()
    {
        var getAllColorsResult = await _mediator.Send(new GetAllColorsQuery());

        return getAllColorsResult.Match(
            colorsResult => Ok(_mapper.Map<List<ColorResponse>>(colorsResult)),
            errors => Problem(errors));
    }
}