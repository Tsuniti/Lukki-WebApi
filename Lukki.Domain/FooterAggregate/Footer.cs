using Lukki.Domain.Common.Models;
using Lukki.Domain.FooterAggregate.ValueObjects;

namespace Lukki.Domain.FooterAggregate;

public sealed class Footer  : AggregateRoot<FooterId>
{
    private readonly List<FooterSection> _sections = new();
    
    public string Name { get; set; }
    
    public string CopyrightText { get; set; }
    
    public IReadOnlyList<FooterSection> Sections => _sections.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Footer(
        FooterId footerId,
        string name,
        string copyrightText,
        List<FooterSection> sections,
        DateTime createdAt
    ) : base(footerId)
    {
        Name = name;
        CopyrightText = copyrightText;
        _sections = sections;
        CreatedAt = createdAt;
    }
    
    public static Footer Create(
        string name,
        string copyrightText,
        List<FooterSection> sections
    )
    {
        return new(
            FooterId.CreateUnique(),
            name,
            copyrightText,
            sections,
            DateTime.UtcNow
        );
    }
    
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Footer()
    {
        // EF Core requires a parameterless constructor for materialization
    }
    #pragma warning restore CS8618
}