using ErrorOr;
using Lukki.Application.Categories.Queries.GetAllCategories;
using Lukki.Application.Footers.Queries.GetFooterByName;
using Lukki.Application.Headers.Commands.CreateHeader;
using Lukki.Application.Headers.Queries.GetAllHeaderNames;
using Lukki.Application.Headers.Queries.GetHeaderByName;
using Lukki.Contracts.Banners;
using Lukki.Contracts.Categories;
using Lukki.Contracts.Footers;
using Lukki.Contracts.Header;
using Lukki.Domain.Common.Enums;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.HeaderAggregate;
using Lukki.Domain.OrderAggregate;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Lukki.Api.Controllers;
using Microsoft.AspNetCore.Mvc;


[Route("header")]
public class HeadersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public HeadersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    [HttpPost]
    [Authorize(Roles = nameof(UserRole.SELLER))]
    [ProducesResponseType(typeof(MyHeader), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateFooter(CreateHeaderFormModel form)
    {
        
        const int maxFileSizeBytes = 20 * 1024; // 20 KB
        
        foreach (var button in form.Buttons)
        {
            if (button.Icon?.Length > maxFileSizeBytes)
            {
                return Problem(new List<Error> {
                    Errors.Header.ImageTooLarge(
                        yourImageSize: button.Icon.Length,
                        maxImageSize: maxFileSizeBytes)
                    });
            }
        }
        if(form.Logo.Length > maxFileSizeBytes)
        {
            return Problem(new List<Error> {
                Errors.Header.ImageTooLarge(
                    yourImageSize: form.Logo.Length,
                    maxImageSize: maxFileSizeBytes)
            });
        }
        if(form.OnHoverLogo.Length > maxFileSizeBytes)
        {
            return Problem(new List<Error> {
                Errors.Header.ImageTooLarge(
                    yourImageSize: form.OnHoverLogo.Length,
                    maxImageSize: maxFileSizeBytes)
            });
        }
        
        var buttons = new List<HeaderIconButtonCommand>(form.Buttons.Count);
        
        foreach (var button in form.Buttons)
        {
            Stream? iconStream = null;
            iconStream = await FileHelpers.ConvertToStreamAsync(button.Icon);
            var mappedButton = _mapper.Map<(HeaderIconButtonFormModel, Stream?), HeaderIconButtonCommand>((button, iconStream));
            buttons.Add(mappedButton);
        }
        
        
        var command = _mapper.Map<(
            CreateHeaderFormModel,
            Stream,
            Stream,
            List<HeaderIconButtonCommand>), CreateHeaderCommand>((
            form,
            await FileHelpers.ConvertToStreamAsync(form.Logo),
            await FileHelpers.ConvertToStreamAsync(form.OnHoverLogo),
            buttons));
        
        
        var createHeaderResult = await _mediator.Send(command);

        
        return createHeaderResult.Match(
            header => Ok(_mapper.Map<HeaderResponse>(header)),
            errors => Problem(errors) 
        );
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MyHeader), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetHeaderByName([FromQuery]GetHeaderRequest request)
    {
        var query = _mapper.Map<GetHeaderByNameQuery>(request);

        var getHeaderByNameResult = await _mediator.Send(query);
        
        var getAllCategoriesResult = await _mediator.Send(new GetAllCategoriesQuery());
        
        var categories = getAllCategoriesResult.Match(
            categoriesResult => _mapper.Map<List<CategoryResponse>>(categoriesResult),
            errors => new List<CategoryResponse>());


        return getHeaderByNameResult.Match(
            headerResult => Ok(_mapper.Map<(MyHeader, List<CategoryResponse>), HeaderResponse>((headerResult, categories))),
            errors => Problem(errors));
    }
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MyHeader), StatusCodes.Status200OK)]
    [Route("names")]
    public async Task<IActionResult> GetAllHeaderNames()
    {
        var getAllHeaderNamesResult = await _mediator.Send(new GetAllHeaderNamesQuery());

        return getAllHeaderNamesResult.Match(
            headerResult => Ok(_mapper.Map<HeaderNamesResponse>(headerResult)),
            errors => Problem(errors));
    }

}