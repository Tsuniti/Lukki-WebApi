using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> IsExistsReviewByCustomerIdAndProductIdAsync(CustomerId customerId, ProductId productId)
    {
        return await _dbContext.Reviews.AnyAsync(r => r.CustomerId == customerId && r.ProductId == productId);
    }
    
    public async Task<Review?> GetByIdAsync(ReviewId id)
    {
        return await _dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    
    public async Task<List<Review>> GetListByReviewIdsAsync(IEnumerable<ReviewId> reviewIds)
    {
        return await _dbContext.Reviews
            .AsNoTracking()
            .Where(p => reviewIds.Contains(p.Id))
            .ToListAsync();
    }
    
}