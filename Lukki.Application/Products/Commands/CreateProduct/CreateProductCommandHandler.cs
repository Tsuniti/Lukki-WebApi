using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.Enums;
using Lukki.Domain.ProductAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;
    private readonly IImageStorageService _imageStorage;

    public CreateProductCommandHandler(IProductRepository productRepository, IImageStorageService imageStorage)
    {
        _productRepository = productRepository;
        _imageStorage = imageStorage;
    }

    public async Task<ErrorOr<Product>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {


        // Create Images
        List<Image> newImages = new();
        
        foreach (var requestImage in request.Images)
        {
            string name = Guid.NewGuid().ToString();
            newImages.Add(Image.Create( await _imageStorage.UploadImageAsync(requestImage, name)));
        }
        
        // Create Product
        var product = Product.Create(
            name: request.Name,
            description: request.Description,
            targetGroup: Enum.Parse<TargetGroup>(request.TargetGroup, true),
            price: Money.Create(
                amount: request.Price.Amount,
                currency: request.Price.Currency
            ),
            categoryId: CategoryId.Create(request.CategoryId),
            images: newImages,
            inStockProducts: request.InStockProducts.ConvertAll(
                inStockProduct =>
                    InStockProduct.Create(
                        quantity: inStockProduct.Quantity,
                        size: inStockProduct.Size
                    )),
            sellerId: UserId.Create(request.SellerId)
        );
        // Persist Product
        await _productRepository.AddAsync(product);
        // Return Product
        return product;
    }
}