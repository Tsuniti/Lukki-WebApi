using Lukki.Domain.Common.Models;

namespace Lukki.Domain.Common.ValueObjects;

public class Image : ValueObject
{
    
    public string Url { get; private set; }

    private Image(string url)
    {
        Url = url;
    }
    
    public static Image Create(string url)
    {
        return new Image(url);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }
}