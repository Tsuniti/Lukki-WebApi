using Lukki.Domain.Common.Models;

namespace Lukki.Domain.MaterialAggregate.ValueObjects;

public sealed class MaterialId : ValueObject
{
    public Guid Value { get; private set; }
    
    private MaterialId(Guid value)
    {
        Value = value;
    }

    public static MaterialId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static MaterialId Create(Guid value)
    {
       return new MaterialId(value);
    }

    public static MaterialId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new MaterialId(guidValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}