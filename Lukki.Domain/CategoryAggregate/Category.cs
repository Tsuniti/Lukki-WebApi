using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId>
{
    public string Name { get; }
    public CategoryId? ParentId { get; private set; }

    private Category(
        CategoryId categoryId,
        string name,
        CategoryId? parentId
    ) : base(categoryId)
    {
        Name = name;
        ParentId = parentId;
    }

    public static Category Create(
        string name,
        CategoryId? parentCategoryId = null
    )
    {
        return new(
            CategoryId.CreateUnique(),
            name,
            parentCategoryId
        );
    }
    
    
#pragma warning disable CS8618
    private Category()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}