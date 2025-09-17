using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.BrandAggregate;

public sealed class Brand : AggregateRoot<BrandId>
{
    public string Name { get; private set; }
    public Image Logo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Brand(
        BrandId brandId,
        string name,
        Image logo,
        DateTime createdAt
    ) : base(brandId)
    {
        Name = name;
        Logo = logo;
        CreatedAt = createdAt;
    }

    public static Brand Create(
        string name,
        Image logo,
        BrandId? parentBrandId = null
    )
    {
        return new(
            BrandId.CreateUnique(),
            name,
            logo,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private Brand()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}