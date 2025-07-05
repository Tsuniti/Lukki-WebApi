using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public class Image : ValueObject
{
    
    public string Url { get; }

    public Image(string url)
    {
        Url = url;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }
}