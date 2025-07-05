using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : ValueObject
{
    Guid Value { get; }
    
    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateUnique()
    {
      return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}