using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId, Guid>
{
    public string Name { get; }
    public CategoryId? ParentCategoryId { get; private set; }

    private Category(
        CategoryId categoryId,
        string name,
        CategoryId? parentCategoryId
    ) : base(categoryId)
    {
        Name = name;
        ParentCategoryId = parentCategoryId;
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