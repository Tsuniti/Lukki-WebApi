using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.PromoCategoryAggregate;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class PromoCategoryRepository : IPromoCategoryRepository
{
    private readonly LukkiDbContext _dbContext;

    public PromoCategoryRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PromoCategory promoCategory)
    {
        _dbContext.Add(promoCategory);
        await _dbContext.SaveChangesAsync();
    }

    public Task<PromoCategory?> GetByName(string name)
    {
        return _dbContext.PromoCategories.FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<List<PromoCategory>> GetListByIdsAsync(IReadOnlyList<PromoCategoryId> ids)
    {
        return await _dbContext.PromoCategories
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }


    public async Task<List<PromoCategory>> GetAllAsync()
    {
        return await _dbContext.PromoCategories.ToListAsync();
    }
}