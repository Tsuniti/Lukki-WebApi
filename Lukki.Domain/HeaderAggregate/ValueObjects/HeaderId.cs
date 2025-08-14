using Lukki.Domain.Common.Models;

namespace Lukki.Domain.HeaderAggregate.ValueObjects;

public sealed class HeaderId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private HeaderId(Guid value)
    {
        Value = value;
    }

    public static HeaderId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static HeaderId Create(Guid value)
    {
        return new HeaderId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}