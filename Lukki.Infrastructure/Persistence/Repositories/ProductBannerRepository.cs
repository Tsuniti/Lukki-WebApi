using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductBannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ProductBannerRepository : IProductBannerRepository
{
    private readonly LukkiDbContext _dbContext;

    public ProductBannerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ProductBanner productBanner)
    {
        _dbContext.Add(productBanner);
        await _dbContext.SaveChangesAsync();
    }

    public Task<ProductBanner?> GetByIdAsync(ProductBannerId id)
    {
        return _dbContext.ProductBanners
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}