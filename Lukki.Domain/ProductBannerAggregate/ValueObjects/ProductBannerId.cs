using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductBannerAggregate.ValueObjects;

public sealed class ProductBannerId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private ProductBannerId(Guid value)
    {
        Value = value;
    }

    public static ProductBannerId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ProductBannerId Create(Guid value)
    {
        return new ProductBannerId(value);
    }

    public static ProductBannerId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new ProductBannerId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}