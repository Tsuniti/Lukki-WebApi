using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ProductAggregate;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LukkiDbContext _dbContext;

    public ProductRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product product)
    {
        _dbContext.Add(product);
        _dbContext.SaveChanges();
    }
}