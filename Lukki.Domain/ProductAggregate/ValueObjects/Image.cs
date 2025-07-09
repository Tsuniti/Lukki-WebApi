using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public class Image : ValueObject
{
    
    public string Url { get; private set; }

    private Image(string url)
    {
        Url = url;
    }
    
    public static Image Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(url));
        }

        return new Image(url);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }
}