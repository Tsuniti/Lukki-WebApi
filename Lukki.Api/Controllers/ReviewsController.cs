using System.Security.Claims;
using Lukki.Application.Reviews.Commands.CreateReview;
using Lukki.Contracts.Reviews;
using Lukki.Domain.ProductAggregate;
using Lukki.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

[Route("reviews")]
public class ReviewsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    
    public ReviewsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
 
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateReview(CreateReviewRequest request)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
        if (string.IsNullOrEmpty(customerId))
        {
            return Unauthorized("Customer ID not found in token");
        }
        
        var command = _mapper.Map<CreateReviewCommand>(request );
        var createReviewResult = await _mediator.Send(command with{ CustomerId = customerId});
        
        return createReviewResult.Match(
            review => Ok(_mapper.Map<ReviewResponse>(review)),
            errors => Problem(errors) 
        );
    }
    
}