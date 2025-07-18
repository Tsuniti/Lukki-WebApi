using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LukkiDbContext _dbContext;

    public ProductRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Product>> GetProductsByProductIdsAsync(IEnumerable<ProductId> productIds)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
    }
}