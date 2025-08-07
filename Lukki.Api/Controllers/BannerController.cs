using Lukki.Api.ApiModels.Banners;
using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Application.Banners.Queries.GetAllBannerNames;
using Lukki.Application.Banners.Queries.GetBannerByName;
using Lukki.Contracts;
using Lukki.Contracts.Banners;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.Common.Enums;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("banner")]
public class BannerController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BannerController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    
    [HttpPost]
    [Authorize(Roles = nameof(UserRole.SELLER))]  // hack: Temporary, until we have an admin
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Banner), StatusCodes.Status200OK)]

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
    [ProducesResponseType(typeof(Banner), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBannerByName([FromQuery]GetBannerRequest request)
    {
        var query = _mapper.Map<GetBannerByNameQuery>(request);

        var getBannerByNameResult = await _mediator.Send(query);

        return getBannerByNameResult.Match(
            bannerResult => Ok(UniversalResponse<BannerResponse>.Create($"Banner {bannerResult.Name} successfully found",_mapper.Map<BannerResponse>(bannerResult))),
            errors => Problem(errors));
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Banner), StatusCodes.Status200OK)]
    [Route("names")]
    public async Task<IActionResult> GetAllBannerNames()
    {
        var getAllBannerNamesResult = await _mediator.Send(new GetAllBannerNamesQuery());

        return getAllBannerNamesResult.Match(
            bannerResult => Ok(UniversalResponse<BannerNamesResponse>.Create($"Banner names successfully found",_mapper.Map<BannerNamesResponse>(bannerResult))),
            errors => Problem(errors));
    }
}