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

    public async Task<(List<Review> Reviews, int TotalItems)> GetPagedAsync(string productId, int pageNumber,
        int itemsPerPage, string sortBy)
    {
        IQueryable<Review> query = _dbContext.Reviews.AsQueryable();


        query = query.Where(r => r.ProductId == ProductId.Create(productId));


        query = sortBy switch // "newest", "rate_asc", "rate_desc"
        {
            "rate_asc" => query.OrderBy(r => r.Rating),
            "rate_desc" => query.OrderByDescending(r => r.Rating),
            _ => query.OrderBy(p => p.Id) // newest or default
        };

        var totalCount = await query.CountAsync();

        var reviews = await query
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();

        return (reviews, totalCount);
    }

    public async Task<List<Review>> GetListByReviewIdsAsync(IEnumerable<ReviewId> reviewIds)
    {
        return await _dbContext.Reviews
            .AsNoTracking()
            .Where(p => reviewIds.Contains(p.Id))
            .ToListAsync();
    }
}