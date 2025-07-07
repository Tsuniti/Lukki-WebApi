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
    private readonly List<CategoryId> _categoryIds = new();
    
    public string Name { get; }
    public string Description { get; }
    public TargetGroup TargetGroup { get; }
    public AverageRating AverageRating { get; }
    public Price Price { get; }
    public UserId SellerId { get; }
    public IReadOnlyList<Image> Images => _images.AsReadOnly();
    public IReadOnlyList<InStockProduct> InStockProducts => _inStockProducts.AsReadOnly();
    public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
    public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Product(
        ProductId productId,
        string name,
        string description,
        TargetGroup targetGroup,
        AverageRating averageRating,
        Price price,
        List<Image> images,
        List<InStockProduct> inStockProducts,
        UserId sellerId,
        DateTime createdAt) : base(productId)
    {
        Name = name;
        Description = description;
        TargetGroup = targetGroup;
        AverageRating = averageRating;
        Price = price;
        _images = images;
        _inStockProducts = inStockProducts;
        SellerId = sellerId;
        CreatedAt = createdAt;
    }
    public static Product Create(
        string name,
        string description,
        TargetGroup targetGroup,
        Price price,
        List<Image> images,
        List<InStockProduct> inStockProducts,
        UserId sellerId
    )
    {
        return new(
            ProductId.CreateUnique(),
            name,
            description,
            targetGroup,
            AverageRating.CreateNew(),
            price,
            images,
            inStockProducts,
            sellerId,
            DateTime.Now);
    }
}