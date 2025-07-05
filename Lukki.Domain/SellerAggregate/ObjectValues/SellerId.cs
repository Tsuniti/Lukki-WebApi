using Lukki.Domain.Common.Models;

namespace Lukki.Domain.SellerAggregate.ObjectValues;

public class SellerId : ValueObject
{
    Guid Value { get; }
    
    private SellerId(Guid value)
    {
        Value = value;
    }

    public static SellerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}