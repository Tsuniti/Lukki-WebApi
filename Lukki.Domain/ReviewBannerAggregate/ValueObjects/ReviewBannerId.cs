using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ReviewBannerAggregate.ValueObjects;

public sealed class ReviewBannerId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private ReviewBannerId(Guid value)
    {
        Value = value;
    }

    public static ReviewBannerId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ReviewBannerId Create(Guid value)
    {
        return new ReviewBannerId(value);
    }

    public static ReviewBannerId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new ReviewBannerId(guidValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}