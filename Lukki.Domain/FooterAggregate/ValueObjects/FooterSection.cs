using Lukki.Domain.Common.Models;

namespace Lukki.Domain.FooterAggregate.ValueObjects;

public class FooterSection : ValueObject
{
    
    private readonly List<FooterLink> _links = new();

    public string Name { get; private set; }
    
    public IReadOnlyList<FooterLink> Links => _links.AsReadOnly();

    public Int16 SortOrder { get; private set; }

    
    private FooterSection(string name, List<FooterLink> links, Int16 sortOrder)
    {
        Name = name;
        _links = links;
        SortOrder = sortOrder;
    }
    
    
    public static FooterSection Create(string name, List<FooterLink> links, Int16 sortOrder)
    {
        return new FooterSection(name, links, sortOrder);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Links;
    }
    
    private FooterSection()
    {
        // EF Core requires a parameterless constructor for materialization
    }
}