using Lukki.Domain.Common.Models;

namespace Lukki.Domain.OrderAggregate.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get; private set; }
    
    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
