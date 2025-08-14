using Lukki.Domain.CategoryAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler :
    IRequestHandler<GetAllCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<ErrorOr<List<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();

        if (categories is null || categories.Count == 0)
        {
            return Errors.Category.NoCategoriesFound();
        }

        return categories;
    }
}