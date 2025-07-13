using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
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