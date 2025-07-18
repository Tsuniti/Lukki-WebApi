using Lukki.Domain.Common.Models;

namespace Lukki.Domain.BannerAggregate.ValueObjects;

public sealed class BannerId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private BannerId(Guid value)
    {
        Value = value;
    }

    public static BannerId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static BannerId Create(Guid value)
    {
        return new BannerId(value);
    }

    public static BannerId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new BannerId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}