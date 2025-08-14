using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.HeaderAggregate.ValueObjects;

public class BurgerMenuLink : ValueObject
{
    public string Text { get; private set; }
    public string Url { get; private set; }
    public Int16 SortOrder { get; private set; }

    
    private BurgerMenuLink(string text, string url, Int16 sortOrder)
    {
        Text = text;
        Url = url;
        SortOrder = sortOrder;
    }
    
    
    public static BurgerMenuLink Create(string text, string url, Int16 sortOrder)
    {
        return new BurgerMenuLink(text, url, sortOrder);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
        yield return Url;
    }
}