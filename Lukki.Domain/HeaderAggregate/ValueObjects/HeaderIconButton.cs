using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.HeaderAggregate.ValueObjects;

public class HeaderIconButton : ValueObject
{
    public Image Icon { get; set; }
    public string Url { get; private set; }
    public Int16 SortOrder { get; private set; }

    
    private HeaderIconButton(Image icon, string url, Int16 sortOrder)
    {
        Icon = icon;
        Url = url;
        SortOrder = sortOrder;
    }
    
    
    public static HeaderIconButton Create(Image icon, string url, Int16 sortOrder)
    {
        return new HeaderIconButton(icon, url, sortOrder);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Icon;
        yield return Url;
    }
}