using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.MaterialAggregate;

public sealed class Material : AggregateRoot<MaterialId>
{
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Material(
        MaterialId materialId,
        string name,
        DateTime createdAt
    ) : base(materialId)
    {
        Name = name;
        CreatedAt = createdAt;
    }

    public static Material Create(
        string name
    )
    {
        return new(
            MaterialId.CreateUnique(),
            name,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private Material()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}