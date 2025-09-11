using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate;
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
}