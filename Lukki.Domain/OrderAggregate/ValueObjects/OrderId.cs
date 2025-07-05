using Lukki.Domain.Common.Models;

namespace Lukki.Domain.OrderAggregate.ValueObjects;

public class OrderId : ValueObject
{
    Guid Value { get; }
    
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
