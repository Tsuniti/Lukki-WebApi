using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Contracts.Banners;
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
    public async Task<IActionResult> CreateBanner(  // possible create only one slide, then add more slides
        [FromForm]CreateBannerRequest request,
        [FromForm]IFormFile image)
    {
        
        var streamImage = await FileHelpers.ConvertToStreamAsync(image);
        
        var command = _mapper.Map<(CreateBannerRequest, Stream), CreateBannerCommand>((request, streamImage));
       
        //var command = _mapper.Map<CreateBannerCommand>(request);

        var createBannerResult = await _mediator.Send(command);
        
        return createBannerResult.Match(
            banner => Ok(_mapper.Map<BannerResponse>(banner)),
            errors => Problem(errors) 
        );
    }
    
}