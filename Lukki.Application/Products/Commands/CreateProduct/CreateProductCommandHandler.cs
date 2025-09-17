using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;
    private readonly IImageStorageService _imageStorage;
    private readonly IImageCompressor _imageCompressor;

    public CreateProductCommandHandler(IProductRepository productRepository, IImageStorageService imageStorage, IImageCompressor imageCompressor)
    {
        _productRepository = productRepository;
        _imageStorage = imageStorage;
        _imageCompressor = imageCompressor;
    }

    public async Task<ErrorOr<Product>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {


        // Create Images
        List<Image> newImages = new();
        
        foreach (var requestImage in command.Images)
        {
            string name = Guid.NewGuid().ToString();
            var compressedImageStream = await _imageCompressor.CompressAsync(requestImage);
            newImages.Add(Image.Create( await _imageStorage.UploadImageAsync(compressedImageStream, name)));
        }
        
        // Create Product
        var product = Product.Create(
            name: command.Name,
            description: command.Description,
            // targetGroup: Enum.Parse<TargetGroup>(command.TargetGroup, true),
            price: Money.Create(
                amount: command.Price.Amount,
                currency: command.Price.Currency
            ),
            categoryId: CategoryId.Create(command.CategoryId),
            promoCategoryIds: command.PromoCategoryIds.ConvertAll(promoCategoryId => PromoCategoryId.Create(promoCategoryId)),
            brandId: BrandId.Create(command.BrandId),
            colorId: ColorId.Create(command.ColorId),
            materialIds: command.MaterialIds.ConvertAll(materialId => MaterialId.Create(materialId)),
            images: newImages,
            inStockProducts: command.InStockProducts.ConvertAll(
                inStockProduct =>
                    InStockProduct.Create(
                        quantity: inStockProduct.Quantity,
                        size: inStockProduct.Size
                    ))
        );
        // Persist Product
        await _productRepository.AddAsync(product);
        // Return Product
        return product;
    }
}