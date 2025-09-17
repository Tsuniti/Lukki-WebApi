using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ColorAggregate.ValueObjects;

public sealed class ColorId : ValueObject
{
    public Guid Value { get; private set; }
    
    private ColorId(Guid value)
    {
        Value = value;
    }

    public static ColorId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ColorId Create(Guid value)
    {
       return new ColorId(value);
    }

    public static ColorId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new ColorId(guidValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}