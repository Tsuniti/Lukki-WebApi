using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ReviewAggregate;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly LukkiDbContext _dbContext;

    public ReviewRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Review review)
    {
        _dbContext.Add(review);
        await _dbContext.SaveChangesAsync();
    }
}