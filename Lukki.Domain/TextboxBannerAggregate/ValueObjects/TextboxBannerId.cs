using Lukki.Domain.Common.Models;

namespace Lukki.Domain.TextboxBannerAggregate.ValueObjects;

public sealed class TextboxBannerId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private TextboxBannerId(Guid value)
    {
        Value = value;
    }

    public static TextboxBannerId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static TextboxBannerId Create(Guid value)
    {
        return new TextboxBannerId(value);
    }

    public static TextboxBannerId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new TextboxBannerId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}