using Lukki.Domain.ProductBannerAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.ProductBannerAggregate;

public sealed class ProductBanner : AggregateRoot<ProductBannerId>
{
    private readonly List<ProductId> _productIds = new();
    
    public string Title { get; private set; }
    public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private ProductBanner(
        ProductBannerId productBannerId,
        string title,
        List<ProductId> productIds,
        DateTime createdAt
    ) : base(productBannerId)
    {
        Title = title;
        _productIds = productIds ?? new List<ProductId>();
        CreatedAt = createdAt;
    }
    
    public static ProductBanner Create(
        string title,
        List<ProductId> productIds
    )
    {
        return new(
            ProductBannerId.CreateUnique(),
            title,
            productIds,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private ProductBanner()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}