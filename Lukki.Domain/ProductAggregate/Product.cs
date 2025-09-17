using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.Events;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<MaterialId> _materialIds = new();
    private readonly List<InStockProduct> _inStockProducts = new();
    private readonly List<Image> _images = new();
    public string Name { get; private set; }
    public string Description { get; private set; }
    // public TargetGroup TargetGroup { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public Money Price { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public BrandId BrandId { get; private set; }
    public ColorId ColorId { get; private set; }
    public IReadOnlyList<MaterialId> MaterialIds => _materialIds.AsReadOnly();
    public IReadOnlyList<Image> Images => _images.AsReadOnly();
    public IReadOnlyList<InStockProduct> InStockProducts => _inStockProducts.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Product(
        ProductId productId,
        string name,
        string description,
        // TargetGroup targetGroup,
        AverageRating averageRating,
        Money price,
        CategoryId categoryId,
        BrandId brandId,
        ColorId colorId,
        List<MaterialId> materialIds,
        List<Image> images,
        List<InStockProduct> inStockProducts,
        DateTime createdAt) : base(productId)
    {
        Name = name;
        Description = description;
        // TargetGroup = targetGroup;
        AverageRating = averageRating;
        Price = price;
        CategoryId = categoryId;
        BrandId = brandId;
        ColorId = colorId;
        _materialIds = materialIds;
        _images = images;
        _inStockProducts = inStockProducts;
        CreatedAt = createdAt;
    }
    public static Product Create(
        string name,
        string description,
        // TargetGroup targetGroup,
        Money price,
        CategoryId categoryId,
        BrandId brandId,
        ColorId colorId,
        List<MaterialId> materialIds,
        List<Image> images,
        List<InStockProduct> inStockProducts
    )
    {
        var product = new Product(
            ProductId.CreateUnique(),
            name,
            description,
            // targetGroup,
            AverageRating.CreateNew(),
            price,
            categoryId,
            brandId,
            colorId,
            materialIds,
            images,
            inStockProducts,
            DateTime.UtcNow);

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