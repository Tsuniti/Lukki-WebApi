using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Common.Models;

namespace Lukki.Application.Services.Currency;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository _repository;
    private readonly IExchangeRateApiClient _apiClient;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

    public ExchangeRateService(IExchangeRateRepository repository, IExchangeRateApiClient apiClient)
    {
        _repository = repository;
        _apiClient = apiClient;
    }

    public async Task<Dictionary<string, decimal>> GetRatesAsync()
    {
        var exchangeRate = await _repository.GetAsync();
        exchangeRate ??= new ExchangeRateData();

        if ((DateTime.UtcNow - exchangeRate.LastUpdated) > _cacheDuration)
        {
            var freshData = await _apiClient.FetchLatestRatesAsync();

            exchangeRate = freshData;
            await _repository.UpdateAsync(exchangeRate);
        }

        return exchangeRate.Rates;
    }
}