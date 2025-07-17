using Lukki.Application.Common.Models;

namespace Lukki.Application.Common.Interfaces.Services.Currency;

public interface IExchangeRateApiClient
{
    Task<ExchangeRateData> FetchLatestRatesAsync();
}