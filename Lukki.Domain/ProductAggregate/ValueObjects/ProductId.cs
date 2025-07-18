using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; private set; }
    
    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ProductId Create(Guid value)
    {
       return new ProductId(value);
    }

    public static ProductId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new ProductId(guidValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}