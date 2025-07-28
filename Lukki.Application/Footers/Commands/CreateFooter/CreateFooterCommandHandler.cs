using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.FooterAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Footers.Commands.CreateFooter;

public class CreateFooterCommandHandler : IRequestHandler<CreateFooterCommand, ErrorOr<Footer>>
{

    private readonly IFooterRepository _footerRepository;
    private readonly IImageStorageService _imageStorage;


    public CreateFooterCommandHandler(IFooterRepository footerRepository, IImageStorageService imageStorage)
    {
        _footerRepository = footerRepository;
        _imageStorage = imageStorage;
    }

    public async Task<ErrorOr<Footer>> Handle(CreateFooterCommand request, CancellationToken cancellationToken)
    {
        
        // Validate
        
        // Create Image
        
        // Create Slides

        var sections = new List<FooterSection>(request.Sections.Count);
        
        foreach (var requestSection in request.Sections)
        {
            var links = new List<FooterLink>(requestSection.Links.Count);
            
            foreach (var requestLink in requestSection.Links)
            {
                Image? icon = null;
                if (requestLink.IconStream is not null)
                {
                    string name = Guid.NewGuid().ToString();
                    icon = Image.Create(await _imageStorage.UploadImageAsync(requestLink.IconStream, name));
                }

                links.Add(
                    FooterLink.Create(
                        text: requestLink.Text,
                        url: requestLink.Url,
                        icon: icon,
                        sortOrder: requestLink.SortOrder));
            }
            
            sections.Add(FooterSection.Create(
                name: requestSection.Name,
                links: links,
                sortOrder: requestSection.SortOrder));
            
        }
        
        
        var footer = Footer.Create(
            name: request.Name,
            copyrightText: request.CopyrightText,
            sections: sections
        );
        // Persist Footer
        await _footerRepository.AddAsync(footer);
        // Return Footer
        return footer;
        
        
    }
}