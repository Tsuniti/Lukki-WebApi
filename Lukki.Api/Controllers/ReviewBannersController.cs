using Lukki.Application.Categories.Queries.GetAllCategories;
using Lukki.Application.ReviewBanners.Commands.CreateReviewBanner;
using Lukki.Application.ReviewBanners.Queries.GetReviewBannerById;
using Lukki.Application.Reviews.Queries.GetReviewsByIds;
using Lukki.Contracts.Banners;
using Lukki.Contracts.ReviewBanners;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.ReviewBannerAggregate;
using Lukki.Domain.ReviewAggregate;
using Lukki.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Error = ErrorOr.Error;

namespace Lukki.Api.Controllers;


[Route("review-banners")]
public class ReviewBannersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ReviewBannersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(CreateReviewBannerResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateReviewBanner(CreateReviewBannerRequest request)
    {
        
        var command = _mapper.Map<CreateReviewBannerCommand>(request);
        

        var createReviewBannerResult = await _mediator.Send(command);
        
        return createReviewBannerResult.Match(
            reviewBanner => Ok(_mapper.Map<CreateReviewBannerResponse>(reviewBanner)),
            errors => Problem(errors) 
        );
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ReviewBannerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReviewBannerById([FromQuery]GetReviewBannerRequest request)
    {
        var query = _mapper.Map<GetReviewBannerByIdQuery>(request);

        var getReviewBannerByIdResult = await _mediator.Send(query);
        
        return getReviewBannerByIdResult.Match(
            reviewBannerResult => Ok(_mapper.Map<ReviewBannerResponse>(reviewBannerResult)),
            errors => Problem(errors));
    }
}