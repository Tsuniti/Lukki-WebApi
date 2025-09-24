using Lukki.Domain.ProductAggregate;
using MediatR;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Products.Queries.GetProductsByIds;

public class GetProductsByIdsQueryHandler : 
    IRequestHandler<GetProductsByIdsQuery, ErrorOr<List<Product>>>
{
    
    private readonly IProductRepository _productRepository;

    public GetProductsByIdsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<List<Product>>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
    {
        var productIds = request.ProductIds
            .Select(id => ProductId.Create(id))
            .ToList();
        
        var products = await _productRepository.GetListByIdsAsync(productIds);
    
        if (products.Count != request.ProductIds.Count)
        {
            var missingIds = productIds.Except(products.Select(p => p.Id));
            return Errors.Product.NotFoundByIds(missingIds);
        }

        return products;
    }
}