using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product> Update(Product product);
    Task<Product> AddRating(Product product, short Rating);
    Task<List<Product>> GetListByProductIdsAsync(IEnumerable<ProductId> productIds);
    Task<Product?> GetByIdAsync(ProductId id);

}