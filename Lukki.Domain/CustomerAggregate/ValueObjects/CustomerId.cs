using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CustomerAggregate.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static CustomerId Create(Guid value)
    {
        return new CustomerId(value);
    }

    public static CustomerId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new CustomerId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}