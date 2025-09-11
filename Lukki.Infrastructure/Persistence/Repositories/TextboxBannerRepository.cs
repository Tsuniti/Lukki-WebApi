using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.TextboxBannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class TextboxBannerRepository : ITextboxBannerRepository
{
    private readonly LukkiDbContext _dbContext;

    public TextboxBannerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TextboxBanner textboxBanner)
    {
        _dbContext.Add(textboxBanner);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TextboxBanner?> GetByNameAsync(string name)
    {
        return await _dbContext.TextboxBanners
            .FirstOrDefaultAsync(b => b.Name == name);
    }
    public Task<TextboxBanner?> GetByIdAsync(TextboxBannerId id)
    {
        return _dbContext.TextboxBanners
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}