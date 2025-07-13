using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ReviewAggregate.ValueObjects;

public sealed class ReviewId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    private ReviewId(Guid value)
    {
        Value = value;
    }

    public static ReviewId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ReviewId Create(Guid value)
    {
        return new ReviewId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}