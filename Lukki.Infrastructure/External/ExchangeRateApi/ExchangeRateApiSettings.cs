namespace Lukki.Infrastructure.External.ExchangeRateApi;

public class ExchangeRateApiSettings
{
    public const string SectionName = "ExchangeRateApiSettings";
    public string Url { get; init; } = null!;
    public string ApiKey { get; init; } = null!;
    public string BaseCurrency { get; init; } = null!;
}