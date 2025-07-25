using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.BannerAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using MediatR;

namespace Lukki.Application.Banners.Commands.CreateBanner;

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, ErrorOr<Banner>>
{

    private readonly IBannerRepository _bannerRepository;
    private readonly IImageStorageService _imageStorage;
    private readonly IImageCompressor _imageCompressor;


    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IImageStorageService imageStorage, IImageCompressor imageCompressor)
    {
        _bannerRepository = bannerRepository;
        _imageStorage = imageStorage;
        _imageCompressor = imageCompressor;
    }

    public async Task<ErrorOr<Banner>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        
        // Validate
        
        // Create Image
        
        // Create Slides

        var slides = new List<Slide>(request.Slides.Count);

        foreach (var requestSlide in request.Slides)
        {
            string name = Guid.NewGuid().ToString();

            var compressedImageStream = await _imageCompressor.CompressAsync(requestSlide.ImageStream);

            Image newImage = Image.Create( await _imageStorage.UploadImageAsync(compressedImageStream, name));

            slides.Add(
                Slide.Create(
                    image: newImage,
                    text: requestSlide.Text,
                    buttonText: requestSlide.ButtonText,
                    buttonUrl: requestSlide.ButtonUrl,
                    sortOrder: requestSlide.SortOrder));
        }
        

        
        // Create Banner
        var banner = Banner.Create(
            name: request.Name,
            slides: slides 
        );
        // Persist Banner
        await _bannerRepository.AddAsync(banner);
        // Return Banner
        return banner;
        
        
    }
}