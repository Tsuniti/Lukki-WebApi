using Lukki.Domain.Common.Models;

namespace Lukki.Domain.OrderAggregate.ValueObjects;

public class OrderId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    
    public static OrderId Create(Guid value)
    {
        return new OrderId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
