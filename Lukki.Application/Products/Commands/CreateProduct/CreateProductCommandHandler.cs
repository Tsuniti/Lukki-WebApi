using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.Enums;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.SellerAggregate;
using MediatR;

namespace Lukki.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask; // hack: !!! Temporarily, until I add asynchronous logic !!!!
        
        // Create Product
        var product = Product.Create(
            name: request.Name,
            description: request.Description,
            targetGroup: Enum.Parse<TargetGroup>(request.TargetGroup, true),
            price: Price.Create(
                amount: request.Price.Amount,
                currency: request.Price.Currency
            ),
            images: request.Images.ConvertAll(image => Image.Create(image)),
            inStockProducts: request.InStockProducts.ConvertAll(
                inStockProduct =>
                    InStockProduct.Create(
                        quantity: inStockProduct.Quantity,
                        size: inStockProduct.Size
                    )),
            sellerId: UserId.Create(request.SellerId)
        );
        // Persist Product
        _productRepository.Add(product);
        // Return Product
        return product;
    }
}