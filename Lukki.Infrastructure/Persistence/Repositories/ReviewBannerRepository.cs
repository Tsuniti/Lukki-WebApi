using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ReviewBannerAggregate;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ReviewBannerRepository : IReviewBannerRepository
{
    private readonly LukkiDbContext _dbContext;

    public ReviewBannerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ReviewBanner reviewBanner)
    {
        _dbContext.Add(reviewBanner);
        await _dbContext.SaveChangesAsync();
    }

    public Task<ReviewBanner?> GetByIdAsync(ReviewBannerId id)
    {
        return _dbContext.ReviewBanners
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}