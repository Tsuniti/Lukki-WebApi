using Lukki.Domain.Common.Models;

namespace Lukki.Domain.BrandAggregate.ValueObjects;

public sealed class BrandId : ValueObject
{
    public Guid Value { get; private set; }
    
    private BrandId(Guid value)
    {
        Value = value;
    }

    public static BrandId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static BrandId Create(Guid value)
    {
       return new BrandId(value);
    }

    public static BrandId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new BrandId(guidValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}