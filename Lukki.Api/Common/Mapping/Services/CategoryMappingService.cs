using Lukki.Contracts.Categories;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;

namespace Lukki.Api.Common.Mapping.Services;

public static class CategoryMappingService
{
    public static List<CategoryResponse> BuildCategoryTree(List<Category> categories)
    {
        var childrenLookup = categories
            .Where(c => c.ParentId != null)
            .ToLookup(
                keySelector: c => c.ParentId!, 
                elementSelector: c => c
            );

        return categories
            .Where(c => c.ParentId == null)
            .Select(category => new CategoryResponse(
                Id: category.Id.Value.ToString(),
                Name: category.Name,
                SubCategories: GetChildren(category.Id, childrenLookup)
            ))
            .ToList();
    }

    private static List<CategoryResponse> GetChildren(
        CategoryId parentId,
        ILookup<CategoryId, Category> childrenLookup)
    {
        if (!childrenLookup.Contains(parentId))
            return new List<CategoryResponse>();

        return childrenLookup[parentId]
            .Select(child => new CategoryResponse(
                Id: child.Id.Value.ToString(),
                Name: child.Name,
                SubCategories: GetChildren(child.Id, childrenLookup)
            ))
            .ToList();
    }
}