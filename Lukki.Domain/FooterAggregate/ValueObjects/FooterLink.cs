using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.FooterAggregate.ValueObjects;

public class FooterLink : ValueObject
{
    public string Text { get; private set; }
    public string Url { get; private set; }
    public Image Icon { get; set; }
    
    public Int16 SortOrder { get; private set; }

    
    private FooterLink(string text, string url, Image icon, Int16 sortOrder)
    {
        Text = text;
        Url = url;
        Icon = icon;
        SortOrder = sortOrder;
    }
    
    
    public static FooterLink Create(string text, string url, Image icon, Int16 sortOrder)
    {
        return new FooterLink(text, url, icon, sortOrder);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
        yield return Url;
        yield return Icon;
    }
}