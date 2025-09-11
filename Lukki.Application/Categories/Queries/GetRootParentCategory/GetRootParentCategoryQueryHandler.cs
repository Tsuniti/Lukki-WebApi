using Lukki.Domain.CategoryAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Categories.Queries.GetRootParentCategory;

public class GetRootParentCategoryQueryHandler : 
    IRequestHandler<GetRootParentCategoryQuery, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetRootParentCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(GetRootParentCategoryQuery query, CancellationToken cancellationToken)
    {
        var rootCategory = await _categoryRepository.GetRootParentAsync(CategoryId.Create(query.Id));
        
        if (rootCategory is null)
        {
            Errors.Category.RootParentNotFound(query.Id);
        }

        return rootCategory;
    }
}