using Lukki.Domain.Common.Models;

namespace Lukki.Domain.PromoCategoryAggregate.ValueObjects;

public sealed class PromoCategoryId : ValueObject
{
    public Guid Value { get; private set; }
    
    private PromoCategoryId(Guid value)
    {
        Value = value;
    }

    public static PromoCategoryId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static PromoCategoryId Create(Guid value)
    {
       return new PromoCategoryId(value);
    }

    public static PromoCategoryId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new PromoCategoryId(guidValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}