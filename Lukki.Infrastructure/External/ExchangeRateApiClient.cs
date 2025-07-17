using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Lukki.Application.Common.Interfaces.Services;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Common.Models;

namespace Lukki.Infrastructure.External;

public class ExchangeRateApiClient : IExchangeRateApiClient
{
    private readonly HttpClient _httpClient;

    public ExchangeRateApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateData> FetchLatestRatesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>("https://v6.exchangerate-api.com/v6/c41ac8c7f418a10d15ddbd05/latest/USD");

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