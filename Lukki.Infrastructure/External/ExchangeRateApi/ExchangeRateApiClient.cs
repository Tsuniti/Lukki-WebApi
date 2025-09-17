using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace Lukki.Infrastructure.External.ExchangeRateApi;

public class ExchangeRateApiClient : IExchangeRateApiClient
{
    private readonly ExchangeRateApiSettings _exchangeRateApiSettings;
    private readonly HttpClient _httpClient;

    public ExchangeRateApiClient(
        HttpClient httpClient,
        IOptions<ExchangeRateApiSettings> exchangeRateOptions)
    {
        _httpClient = httpClient;
        _exchangeRateApiSettings = exchangeRateOptions.Value;
    }

    public async Task<ExchangeRateData> FetchLatestRatesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>(
            $"{_exchangeRateApiSettings.Url}{_exchangeRateApiSettings.ApiKey}/latest/{_exchangeRateApiSettings.BaseCurrency}");

        if (response is null || response.Result != "success")
            throw new Exception("Failed to fetch exchange rates");

        return new ExchangeRateData
        {
            BaseCurrency = response.BaseCode,
            Rates = response.ConversionRates,
            LastUpdated = DateTime.UtcNow
        };
    }

    private class ApiResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; } = default!;
        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; } = default!;
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; } = default!;
    }
}