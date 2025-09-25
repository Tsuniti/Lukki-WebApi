using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Money
    {
        public static Error SumDifferentCurrencies(string firstCurrency, string secondCurrency) => Error.Validation(
            code: "Money.SumDifferentCurrencies",
            description: $"You can't add different currencies together. You trying add: {firstCurrency} to {secondCurrency}" );

        public static Error CurrencyNotSupported(string currency) => Error.Validation(
            code: "Money.CurrencyNotSupported",
            description: $"Currency {currency} not supported");
    }
}