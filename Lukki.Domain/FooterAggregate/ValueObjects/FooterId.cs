using Lukki.Domain.Common.Models;

namespace Lukki.Domain.FooterAggregate.ValueObjects;

public sealed class FooterId : ValueObject
{
    public Guid Value { get; protected set; }
    
    private FooterId(Guid value)
    {
        Value = value;
    }

    public static FooterId CreateUnique()
    {
      return new(Guid.NewGuid());
    }
    
    public static FooterId Create(Guid value)
    {
        return new FooterId(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}