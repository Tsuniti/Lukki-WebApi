using Lukki.Domain.ProductAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    void Add(Product product);
}