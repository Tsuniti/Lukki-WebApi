using Lukki.Application.Products.Common;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<Product> AddRating(Product product, short rating);
    Task<List<Product>> GetListByIdsAsync(IEnumerable<ProductId> productIds);
    Task<(IReadOnlyList<Product> Products, int TotalItems)> GetPagedAsync(ProductFilter filter);
    Task<Product?> GetByIdAsync(ProductId id);

}