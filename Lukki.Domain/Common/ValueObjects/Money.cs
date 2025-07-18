using Lukki.Domain.Common.Models;

namespace Lukki.Domain.Common.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set;  }
    public string Currency { get; private set; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }
    
    public static Money Create(decimal amount, string currency)
    {
        return new Money(amount, currency);
    }

    public Money Add(Money money)
    {
        if (Currency != money.Currency)
        {
            throw new InvalidOperationException(
                $"Cannot add money with different currencies: {Currency} and {money.Currency}");
        }
        return new Money(Amount + money.Amount, Currency);
    }
    public override string ToString() => $"{Amount} {Currency}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}