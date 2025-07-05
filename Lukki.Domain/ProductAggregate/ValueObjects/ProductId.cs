using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : ValueObject
{
    Guid Value { get; }
    
    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
      return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}