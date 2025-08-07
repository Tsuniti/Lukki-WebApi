using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BannerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class BannerRepository : IBannerRepository
{
    private readonly LukkiDbContext _dbContext;

    public BannerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Banner banner)
    {
        _dbContext.Add(banner);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Banner?> GetByNameAsync(string name)
    {
        return await _dbContext.Banners
            .FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<List<string>> GetAllNamesAsync()
    {
        return await _dbContext.Banners
            .Select(b => b.Name)
            .ToListAsync();
    }
}