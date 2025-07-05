using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId>
{
    
    private readonly List<Category> _subCategories = new();

    public string Name { get; }
    public CategoryId? ParentCategoryId { get; private set; }
    public IReadOnlyCollection<Category> SubCategories => _subCategories;

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
}