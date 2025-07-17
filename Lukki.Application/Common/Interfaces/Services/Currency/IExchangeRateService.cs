namespace Lukki.Application.Common.Interfaces.Services.Currency;

public interface IExchangeRateService
{
    Task<Dictionary<string, decimal>> GetRatesAsync();
}