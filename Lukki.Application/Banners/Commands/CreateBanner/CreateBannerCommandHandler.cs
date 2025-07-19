using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
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


    public CreateBannerCommandHandler(IBannerRepository bannerRepository, IImageStorageService imageStorage)
    {
        _bannerRepository = bannerRepository;
        _imageStorage = imageStorage;
    }

    public async Task<ErrorOr<Banner>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        
        // Validate
        
        // Create Image
        string name = Guid.NewGuid().ToString();

        Image newImage = Image.Create( await _imageStorage.UploadImageAsync(request.Slide.ImageStream, name));
        
        
        // Create Banner
        var banner = Banner.Create(
            name: request.Name,
            slide: Slide.Create(
                image: newImage,
                text: request.Slide.Text,
                buttonText: request.Slide.ButtonText,
                buttonUrl: request.Slide.ButtonUrl,
                sortOrder: request.Slide.SortOrder)
        );
        // Persist Banner
        await _bannerRepository.AddAsync(banner);
        // Return Banner
        return banner;
        
        
    }
}