using Lukki.Domain.Common.Models;

namespace Lukki.Domain.Common.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }
    
    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }
    public static UserId Create(string value)
    {
        if (!Guid.TryParse(value, out var guidValue))
        {
            throw new ArgumentException("Invalid GUID format.", nameof(value));
        }
        
        return new UserId(guidValue);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}