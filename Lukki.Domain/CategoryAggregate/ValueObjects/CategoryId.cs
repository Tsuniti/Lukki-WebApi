using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }
    
    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static CategoryId Create(Guid value)
    {
        return new CategoryId(value);
    }

    public static CategoryId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new CategoryId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}