using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ProductAggregate;

namespace Lukki.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new();
    public void Add(Product product)
    {
        _products.Add(product);
    }
}