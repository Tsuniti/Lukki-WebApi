using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using MediatR;

namespace Lukki.Application.TextboxBanners.Commands.CreateTextboxBanner;

public class CreateTextboxBannerCommandHandler : IRequestHandler<CreateTextboxBannerCommand, ErrorOr<TextboxBanner>>
{

    private readonly ITextboxBannerRepository _textboxBannerRepository;
    private readonly IImageStorageService _imageStorage;
    private readonly IImageCompressor _imageCompressor;


    public CreateTextboxBannerCommandHandler(ITextboxBannerRepository textboxBannerRepository, IImageStorageService imageStorage, IImageCompressor imageCompressor)
    {
        _textboxBannerRepository = textboxBannerRepository;
        _imageStorage = imageStorage;
        _imageCompressor = imageCompressor;
    }

    public async Task<ErrorOr<TextboxBanner>> Handle(CreateTextboxBannerCommand command, CancellationToken cancellationToken)
    {
        
        // Validate
        
        if (await _textboxBannerRepository.GetByNameAsync(command.Name) is not null)
        {
            return Errors.TextboxBanner.DuplicateName(command.Name);
        }
        
        // Create Image
        
        string imageName = Guid.NewGuid().ToString();

        var compressedImageStream = await _imageCompressor.CompressAsync(command.BackgroundImageStream);

        Image newImage = Image.Create( await _imageStorage.UploadImageAsync(compressedImageStream, imageName));
        
        // Create TextboxBanner
        var textboxBanner = TextboxBanner.Create(
            name: command.Name,
            text: command.Text,
            description: command.Description,
            placeholder: command.Placeholder,
            buttonText: command.ButtonText,
            background: newImage
        );
        // Persist TextboxBanner
        await _textboxBannerRepository.AddAsync(textboxBanner);
        // Return TextboxBanner
        return textboxBanner;
        
        
    }
}