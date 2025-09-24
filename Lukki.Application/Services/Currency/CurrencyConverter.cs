// using Lukki.Application.Common.Interfaces.Services.Currency;
// using Lukki.Domain.Common.ValueObjects;
//
// namespace Lukki.Application.Services.Currency;
//
// public class CurrencyConverter : ICurrencyConverter
// {
//     private readonly IExchangeRateService _exchangeRateService;
//
//     public CurrencyConverter(IExchangeRateService exchangeRateService)
//     {
//         _exchangeRateService = exchangeRateService;
//     }
//
//     public async Task<Money> ConvertAsync(Money money, string toCurrency)
//     {
//         if (money.Currency == toCurrency)
//             return money;
//         
//         var rates = await _exchangeRateService.GetRatesAsync();
//
//         if (!rates.ContainsKey(money.Currency) || !rates.ContainsKey(toCurrency))
//             throw new Exception("Currency not supported");
//
//         var usdAmount = money.Amount / rates[money.Currency];
//         var converted = usdAmount * rates[toCurrency];
//
//         var result = Money.Create(converted, toCurrency);
//
//         return result;
//     }
// }