using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task AddAsync(Product product);

    Task<List<Product>> GetProductsByProductIdsAsync(IEnumerable<ProductId> productIds);
}