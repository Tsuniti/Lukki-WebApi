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
            Errors.Errors.Money.SumDifferentCurrencies(Currency, money.Currency);
        }
        return new Money(Amount + money.Amount, Currency);
    }

    public void Convert(string toCurrency, Dictionary<string, decimal> rates)
    {
        if (Currency == toCurrency)
            return;

        if (!rates.ContainsKey(Currency))
            Errors.Errors.Money.CurrencyNotSupported(Currency);

        if (!rates.ContainsKey(toCurrency))
            Errors.Errors.Money.CurrencyNotSupported(toCurrency);

        var usdAmount = Amount / rates[Currency];
        var converted = usdAmount * rates[toCurrency];

        Amount = converted;
        Currency = toCurrency;
    }
    
    public override string ToString() => $"{Amount} {Currency}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}