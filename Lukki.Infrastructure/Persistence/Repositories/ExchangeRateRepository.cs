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
        
        var entity = new ExchangeRate
        {
            Id = 1, // всегда один
            BaseCurrency = exchangeRate.BaseCurrency,
            Rates = exchangeRate.Rates,
            LastUpdated = exchangeRate.LastUpdated
        };
        _dbContext.Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(ExchangeRateData exchangeRate)
    {
        if (await _dbContext.ExchangeRates.AnyAsync())
        {
            throw new InvalidOperationException("Only one exchange rate record is allowed.");
        }

        var entity = new ExchangeRate
        {
            Id = 1, // всегда один
            BaseCurrency = exchangeRate.BaseCurrency,
            Rates = exchangeRate.Rates,
            LastUpdated = exchangeRate.LastUpdated
        };

        _dbContext.ExchangeRates.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}