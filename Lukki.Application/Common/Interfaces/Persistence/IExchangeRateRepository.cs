using Lukki.Application.Common.Models;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IExchangeRateRepository
{
    Task<ExchangeRateData?> GetAsync();
    public Task UpdateAsync(ExchangeRateData exchangeRate);
}