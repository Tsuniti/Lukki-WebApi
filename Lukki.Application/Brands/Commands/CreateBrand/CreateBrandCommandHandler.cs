using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.BrandAggregate;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using MediatR;

namespace Lukki.Application.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ErrorOr<Brand>>
{
    
    private readonly IBrandRepository _brandRepository;
    private readonly IImageStorageService _imageStorage;
    private readonly IImageCompressor _imageCompressor;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IImageStorageService imageStorage, IImageCompressor imageCompressor)
    {
        _brandRepository = brandRepository;
        _imageStorage = imageStorage;
        _imageCompressor = imageCompressor;
    }

    public async Task<ErrorOr<Brand>> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        
        
        // Validate the brand doesn't exist
        if (await _brandRepository.GetByName(command.Name) is not null)
        {
            return Errors.Brand.DuplicateName(command.Name);
        }
        
        string name = Guid.NewGuid().ToString();
        var compressedImageStream = await _imageCompressor.CompressAsync(command.Image);
        var image = Image.Create( await _imageStorage.UploadImageAsync(compressedImageStream, name));
        
        
        // Create Brand
        var brand = Brand.Create(
            name: command.Name,
            logo: image
        );
        // Persist Brand
        await _brandRepository.AddAsync(brand);
        // Return Brand
        return brand;

    }
}