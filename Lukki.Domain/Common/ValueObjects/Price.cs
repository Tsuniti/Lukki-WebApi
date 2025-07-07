using Lukki.Domain.Common.Models;

namespace Lukki.Domain.Common.ValueObjects;

public class Price : ValueObject
{
    public decimal Amount { get; private set;  }
    public string Currency { get; private set; }

    private Price(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        }
        
        if (currency.Length != 3 || !currency.All(char.IsLetter))
        {
            throw new ArgumentException("Currency code must be a 3-letter ISO 4217 code.", nameof(currency));
        }

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }
    
    public static Price Create(decimal amount, string currency)
    {
        return new Price(amount, currency);
    }

    public override string ToString() => $"{Amount} {Currency}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}