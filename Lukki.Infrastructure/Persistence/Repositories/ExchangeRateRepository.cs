using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Models;
using Lukki.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly LukkiDbContext _dbContext;

    public ExchangeRateRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    public async Task<ExchangeRateData?> GetAsync()
    {
        var entity = await _dbContext.ExchangeRates.FirstOrDefaultAsync();

        if (entity is null)
            return null;

        return new ExchangeRateData
        {
            BaseCurrency = entity.BaseCurrency,
            Rates = entity.Rates,
            LastUpdated = entity.LastUpdated
        };
    }

    public async Task UpdateAsync(ExchangeRateData exchangeRate)
    {

        var tracked = _dbContext.ExchangeRates.Local.FirstOrDefault(e => e.Id == 1);

        if (tracked != null)
        {
            // If it is already tracking, we just update the fields
            tracked.BaseCurrency = exchangeRate.BaseCurrency;
            tracked.Rates = exchangeRate.Rates;
            tracked.LastUpdated = exchangeRate.LastUpdated;
        }
        else
        {
            // if it does not track, load the existing from the database
            var existing = await _dbContext.ExchangeRates.FirstOrDefaultAsync(e => e.Id == 1);
            if (existing != null)
            {
                existing.BaseCurrency = exchangeRate.BaseCurrency;
                existing.Rates = exchangeRate.Rates;
                existing.LastUpdated = exchangeRate.LastUpdated;
            }
            else
            {
                // if not in the database, add a new
                var entity = new ExchangeRate
                {
                    Id = 1,
                    BaseCurrency = exchangeRate.BaseCurrency,
                    Rates = exchangeRate.Rates,
                    LastUpdated = exchangeRate.LastUpdated
                };
                _dbContext.ExchangeRates.Add(entity);
            }
        }

        await _dbContext.SaveChangesAsync();

    }
}