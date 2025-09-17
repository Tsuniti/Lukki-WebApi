using Lukki.Application.TextboxBanners.Commands.CreateTextboxBanner;
using Lukki.Application.TextboxBanners.Queries.GetTextboxBannerById;
using Lukki.Contracts.TextboxBanners;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("textbox-banners")]
public class TextboxBannersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public TextboxBannersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(TextboxBanner), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateTextboxBanner(
        [FromForm]CreateTextboxBannerRequest request,
        [FromForm]IFormFile background)
    {
        
        
        
            var imageStream = await FileHelpers.ConvertToStreamAsync(background);
            
            

        var command = _mapper.Map<(CreateTextboxBannerRequest, Stream), CreateTextboxBannerCommand>((request, imageStream));
        

        var createTextboxBannerResult = await _mediator.Send(command);
        
        return createTextboxBannerResult.Match(
            TextboxBanner => Ok(_mapper.Map<TextboxBannerResponse>(TextboxBanner)),
            errors => Problem(errors) 
        );
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TextboxBanner), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTextboxBannerById([FromQuery]GetTextboxBannerRequest request)
    {
        var query = _mapper.Map<GetTextboxBannerByIdQuery>(request);

        var getTextboxBannerByNameResult = await _mediator.Send(query);

        return getTextboxBannerByNameResult.Match(
            TextboxBannerResult => Ok(_mapper.Map<TextboxBannerResponse>(TextboxBannerResult)),
            errors => Problem(errors));
    }
    
}