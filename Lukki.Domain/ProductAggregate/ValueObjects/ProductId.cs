using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static ProductId Create(Guid value)
    {
       return new ProductId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}