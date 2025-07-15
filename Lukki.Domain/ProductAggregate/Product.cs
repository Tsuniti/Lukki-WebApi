using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate.Enums;
using Lukki.Domain.ProductAggregate.Events;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;
namespace Lukki.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    private readonly List<InStockProduct> _inStockProducts = new();
    private readonly List<Image> _images = new();
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TargetGroup TargetGroup { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public Price Price { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public UserId SellerId { get; private set; }
    public IReadOnlyList<Image> Images => _images.AsReadOnly();
    public IReadOnlyList<InStockProduct> InStockProducts => _inStockProducts.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Product(
        ProductId productId,
        string name,
        string description,
        TargetGroup targetGroup,
        AverageRating averageRating,
        Price price,
        CategoryId categoryId,
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
        CategoryId = categoryId;
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
        CategoryId categoryId,
        List<Image> images,
        List<InStockProduct> inStockProducts,
        UserId sellerId
    )
    {
        var product = new Product(
            ProductId.CreateUnique(),
            name,
            description,
            targetGroup,
            AverageRating.CreateNew(),
            price,
            categoryId,
            images,
            inStockProducts,
            sellerId,
            DateTime.Now);

        product.AddDomainEvent(new ProductCreated(product));

        return product;

    }
    
    
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}