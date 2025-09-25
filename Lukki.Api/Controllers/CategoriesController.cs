using Lukki.Application.Categories.Commands.CreateCategory;
using Lukki.Application.Categories.Queries.GetAllCategories;
using Lukki.Contracts.Categories;
using Lukki.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("categories")]
public class CategoriesController : ApiController
{
    
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CategoriesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        
        var createCategoryResult = await _mediator.Send(command);

        
        return createCategoryResult.Match(
            category => Ok(_mapper.Map<CategoryResponse>(category)),
            errors => Problem(errors) 
        );
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesResult = await _mediator.Send(new GetAllCategoriesQuery());

        return getAllCategoriesResult.Match(
            categoriesResult => Ok(_mapper.Map<List<CategoryResponse>>(categoriesResult)),
            errors => Problem(errors));
    }
}