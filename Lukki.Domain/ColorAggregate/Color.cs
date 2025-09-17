using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.ColorAggregate;

public sealed class Color : AggregateRoot<ColorId>
{
    public string Name { get; private set; }
    public string? HexColorCode { get; private set; }
    public Image? Icon { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Color(
        ColorId colorId,
        string name,
        string? hexColorCode,
        Image? icon,
        DateTime createdAt
    ) : base(colorId)
    {
        Name = name;
        HexColorCode = hexColorCode;
        Icon = icon;
        CreatedAt = createdAt;
    }

    public static Color Create(
        string name,
        string? hexColorCode,
        Image? icon,
        ColorId? parentColorId = null
    )
    {
        return new(
            colorId: ColorId.CreateUnique(),
            name: name,
            hexColorCode: hexColorCode,
            icon: icon,
            createdAt: DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private Color()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}