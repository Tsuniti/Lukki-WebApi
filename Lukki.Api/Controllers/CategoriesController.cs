using Lukki.Application.Categories.Commands.CreateCategory;
using Lukki.Contracts.Categories;
using Lukki.Domain.Common.Enums;
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
    [Authorize(Roles = nameof(UserRole.SELLER))] // Temporary, until we have an admin
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        
        var createCategoryResult = await _mediator.Send(command);

        
        return createCategoryResult.Match(
            category => Ok(_mapper.Map<CategoryResponse>(category)),
            errors => Problem(errors) 
        );
    }
}