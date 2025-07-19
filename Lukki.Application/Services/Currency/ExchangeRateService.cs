using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;

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

        if (exchangeRate == null || (DateTime.UtcNow - exchangeRate.LastUpdated) > _cacheDuration)
        {
            var freshData = await _apiClient.FetchLatestRatesAsync();
            
            if (exchangeRate == null)
            {
                await _repository.AddAsync(freshData);
            }
            else
            {
                exchangeRate.Rates = freshData.Rates;
                exchangeRate.BaseCurrency = freshData.BaseCurrency;
                exchangeRate.LastUpdated = freshData.LastUpdated;
                await _repository.UpdateAsync(exchangeRate);
            }

            exchangeRate = freshData;
        }

        return exchangeRate.Rates;
    }
}