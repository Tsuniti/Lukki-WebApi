using Lukki.Application.PromoCategories.Commands.CreatePromoCategory;
using Lukki.Application.PromoCategories.Queries.GetAllPromoCategories;
using Lukki.Contracts.PromoCategories;
using Lukki.Domain.PromoCategoryAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("promo-categories")]
public class PromoCategoriesController : ApiController
{
    
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public PromoCategoriesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(PromoCategoryResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreatePromoCategory(CreatePromoCategoryRequest request)
    {
        var command = _mapper.Map<CreatePromoCategoryCommand>(request);
        
        var createPromoCategoryResult = await _mediator.Send(command);

        
        return createPromoCategoryResult.Match(
            promoCategory => Ok(_mapper.Map<PromoCategoryResponse>(promoCategory)),
            errors => Problem(errors) 
        );
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<PromoCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPromoCategories()
    {
        var getAllPromoCategoriesResult = await _mediator.Send(new GetAllPromoCategoriesQuery());

        return getAllPromoCategoriesResult.Match(
            promoCategoriesResult => Ok(_mapper.Map<List<PromoCategoryResponse>>(promoCategoriesResult)),
            errors => Problem(errors));
    }
}