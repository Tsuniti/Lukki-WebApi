using Lukki.Domain.Common.Models;

namespace Lukki.Domain.CustomerAggregate.ValueObjects;

public class CustomerId : ValueObject
{
    Guid Value { get; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
    
