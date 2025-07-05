using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ReviewAggregate.ValueObjects;

public sealed class ReviewId : ValueObject
{
    Guid Value { get; }
    
    private ReviewId(Guid value)
    {
        Value = value;
    }

    public static ReviewId CreateUnique()
    {
      return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}