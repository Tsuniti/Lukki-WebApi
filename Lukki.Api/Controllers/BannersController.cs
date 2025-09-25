using Lukki.Api.ApiModels.Banners;
using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Application.Banners.Queries.GetAllBannerNames;
using Lukki.Application.Banners.Queries.GetBannerByName;
using Lukki.Contracts.Banners;
using Lukki.Domain.BannerAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("banners")]
public class BannersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BannersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(BannerResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateBanner([FromForm] CreateBannerFormModel form)
    {
        var slides = new List<SlideCommand>();

        foreach (var slide in form.Slides)
        {
            var imageStream = await FileHelpers.ConvertToStreamAsync(slide.Image);

            var mappedSlide = _mapper.Map<(SlideFormModel, Stream), SlideCommand>((slide, imageStream));
            
            slides.Add(mappedSlide);
        }
        
        var command = _mapper.Map<(CreateBannerFormModel, List<SlideCommand>), CreateBannerCommand>((form, slides));
        

        var createBannerResult = await _mediator.Send(command);
        
        return createBannerResult.Match(
            banner => Ok(_mapper.Map<BannerResponse>(banner)),
            errors => Problem(errors) 
        );
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BannerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBannerByName([FromQuery]GetBannerRequest request)
    {

        var query = _mapper.Map<GetBannerByNameQuery>(request);

        var getBannerByNameResult = await _mediator.Send(query);

        return getBannerByNameResult.Match(
            bannerResult => Ok(_mapper.Map<BannerResponse>(bannerResult)),
            errors => Problem(errors));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BannerNamesResponse), StatusCodes.Status200OK)]
    [Route("names")]
    public async Task<IActionResult> GetAllBannerNames()
    {
        var getAllBannerNamesResult = await _mediator.Send(new GetAllBannerNamesQuery());

        return getAllBannerNamesResult.Match(
            bannerResult => Ok(_mapper.Map<BannerNamesResponse>(bannerResult)),
            errors => Problem(errors));
    }
}