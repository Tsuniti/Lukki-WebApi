using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.ColorAggregate;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using MediatR;

namespace Lukki.Application.Colors.Commands.CreateColor;

public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, ErrorOr<Color>>
{
    
    private readonly IColorRepository _colorRepository;
    private readonly IImageStorageService _imageStorage;
    private readonly IImageCompressor _imageCompressor;

    public CreateColorCommandHandler(IColorRepository colorRepository, IImageStorageService imageStorage, IImageCompressor imageCompressor)
    {
        _colorRepository = colorRepository;
        _imageStorage = imageStorage;
        _imageCompressor = imageCompressor;
    }

    public async Task<ErrorOr<Color>> Handle(CreateColorCommand command, CancellationToken cancellationToken)
    {
        
        if(command.HexColorCode is null && command.Image is null)
        {
            return Errors.Color.NoColorRepresentation();
        }
        
        if(command.HexColorCode is not null && command.Image is not null)
        {
            return Errors.Color.MultipleColorRepresentations();
        }
        
        // Validate the color doesn't exist
        if (await _colorRepository.GetByName(command.Name) is not null)
        {
            return Errors.Color.DuplicateName(command.Name);
        }
        
        Image? image = null;
        if (command.Image is not null)
        {
            string name = Guid.NewGuid().ToString();
            var compressedImageStream = await _imageCompressor.CompressAsync(command.Image);
            image = Image.Create(await _imageStorage.UploadImageAsync(compressedImageStream, name));
        }

        // Create Color
        var color = Color.Create(
            name: command.Name,
            hexColorCode: command.HexColorCode,
            icon: image
        );
        // Persist Color
        await _colorRepository.AddAsync(color);
        // Return Color
        return color;

    }
}