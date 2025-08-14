using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.HeaderAggregate;
using Lukki.Domain.HeaderAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Headers.Commands.CreateHeader;

public class CreateHeaderCommandHandler : IRequestHandler<CreateHeaderCommand, ErrorOr<MyHeader>>
{

    private readonly IHeaderRepository _headerRepository;
    private readonly IImageStorageService _imageStorage;


    public CreateHeaderCommandHandler(IHeaderRepository headerRepository, IImageStorageService imageStorage)
    {
        _headerRepository = headerRepository;
        _imageStorage = imageStorage;
    }

    public async Task<ErrorOr<MyHeader>> Handle(CreateHeaderCommand command, CancellationToken cancellationToken)
    {
        
        // Validate
        
        if (await _headerRepository.GetByNameAsync(command.Name) is not null)
        {
            return Errors.Header.DuplicateName(command.Name);
        }
        
        // Create Image
        
        var buttons = new List<HeaderIconButton>(command.Buttons.Count);
        
        foreach(var requestButton in command.Buttons)
        {
            Image? icon = null;
            {
                string name = Guid.NewGuid().ToString();
                icon = Image.Create(await _imageStorage.UploadImageAsync(requestButton.IconStream, name));
            }
            buttons.Add(
                HeaderIconButton.Create(
                    icon: icon,
                    url: requestButton.Url,
                    sortOrder: requestButton.SortOrder
                )
            );
        }
        
        var burgerMenuLinks = new List<BurgerMenuLink>(command.BurgerMenuLinks.Count);
        
        foreach (var requestLink in command.BurgerMenuLinks)
        {
            burgerMenuLinks.Add(
                BurgerMenuLink.Create(
                    text: requestLink.Text,
                    url: requestLink.Url,
                    sortOrder: requestLink.SortOrder
                )
            );
        }
        
        var header = MyHeader.Create(
            name: command.Name,
            logo: Image.Create(await _imageStorage.UploadImageAsync(command.LogoStream, Guid.NewGuid().ToString())),
            onHoverLogo: Image.Create(await _imageStorage.UploadImageAsync(command.OnHoverLogoStream, Guid.NewGuid().ToString())),
            burgerMenuLinks: burgerMenuLinks,
            buttons: buttons
        );
        
        // Persist Header
        await _headerRepository.AddAsync(header);
        // Return Header
        return header;
        
        
    }
}