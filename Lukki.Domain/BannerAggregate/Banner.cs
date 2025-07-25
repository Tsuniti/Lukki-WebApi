using Lukki.Domain.BannerAggregate.ValueObjects;
using Lukki.Domain.Common.Models;

namespace Lukki.Domain.BannerAggregate;

public sealed class Banner : AggregateRoot<BannerId>
{
    private readonly List<Slide> _slides = new();
    
    public string Name { get; private set; }
    public IReadOnlyList<Slide> Slides => _slides.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Banner(
        BannerId bannerId,
        string name,
        List<Slide> slides,
        DateTime createdAt
    ) : base(bannerId)
    {
        Name = name;
        _slides = slides ?? new List<Slide>();
        CreatedAt = createdAt;
    }
    
    public static Banner Create(
        String name,
        List<Slide> slides
    )
    {
        return new(
            BannerId.CreateUnique(),
            name,
            slides,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private Banner()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}