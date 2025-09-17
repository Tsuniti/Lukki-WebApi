using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.PromoCategoryAggregate;

public sealed class PromoCategory : AggregateRoot<PromoCategoryId>
{
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private PromoCategory(
        PromoCategoryId promoCategoryId,
        string name,
        DateTime createdAt
    ) : base(promoCategoryId)
    {
        Name = name;
        CreatedAt = createdAt;
    }

    public static PromoCategory Create(
        string name
    )
    {
        return new(
            PromoCategoryId.CreateUnique(),
            name,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private PromoCategory()
    {
        // EF Core requires a parameterless constructor for promoCategoryization
    }
#pragma warning restore CS8618
}