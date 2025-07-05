using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate.Enums;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<InStockProduct> _inStockProducts = new();
    private readonly List<Image> _images = new();
    private readonly List<ReviewId> _reviewIds = new();
    private readonly List<CategoryId> _categoriesIds = new();
    
    public string Name { get; }
    public string Description { get; }
    public TargetGroup TargetGroup { get; }
    public float AverageRating { get; }
    public Price Price { get; }
    public IReadOnlyList<Image> Images => _images.AsReadOnly();
    public IReadOnlyList<InStockProduct> InStockProducts => _inStockProducts.AsReadOnly();
    public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
    public IReadOnlyList<CategoryId> CategoriesIds => _categoriesIds.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Product(
        ProductId productId,
        string name,
        string description,
        TargetGroup targetGroup,
        float averageRating,
        Price price,
        DateTime createdAt
    ) : base(productId)
    {
        Name = name;
        Description = description;
        TargetGroup = targetGroup;
        AverageRating = averageRating;
        Price = price;
        CreatedAt = createdAt;
    }
    public static Product Create(
        string name,
        string description,
        TargetGroup targetGroup,
        float averageRating,
        Price price
    )
    {
        return new(
            ProductId.CreateUnique(),
            name,
            description,
            targetGroup,
            averageRating,
            price,
            DateTime.Now);
    }
}