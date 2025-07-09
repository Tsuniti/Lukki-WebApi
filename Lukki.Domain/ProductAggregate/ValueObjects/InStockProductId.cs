using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public sealed class InStockProductId : ValueObject
{
    public Guid Value { get; private set; }
    
    private InStockProductId(Guid value)
    {
        Value = value;
    }

    public static InStockProductId CreateUnique()
    {
      return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}