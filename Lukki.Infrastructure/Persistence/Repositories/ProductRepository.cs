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

    public Task<Product> AddRating(Product product, short Rating)
    {
        product.AverageRating.AddNewRating(Rating);
        return Update(product);
    }

    public async Task<List<Product>> GetListByProductIdsAsync(IEnumerable<ProductId> productIds)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
    }
    public async Task<Product?> GetByIdAsync(ProductId id)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product> Update(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
}