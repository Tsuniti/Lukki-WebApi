using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BannerAggregate;

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
}