using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.TextboxBannerAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.TextboxBanners.Queries.GetTextboxBannerById;

public class GetTextboxBannerByIdQueryHandler : 
    IRequestHandler<GetTextboxBannerByIdQuery, ErrorOr<TextboxBanner>>
{
    
    private readonly ITextboxBannerRepository _textboxBannerRepository;


    public GetTextboxBannerByIdQueryHandler(ITextboxBannerRepository textboxBannerRepository)
    {
        _textboxBannerRepository = textboxBannerRepository;
    }

    public async Task<ErrorOr<TextboxBanner>> Handle(GetTextboxBannerByIdQuery query, CancellationToken cancellationToken)
    {
        
        if (await _textboxBannerRepository.GetByIdAsync(TextboxBannerId.Create(query.Id)) is not TextboxBanner textboxBanner)
        {
            return Errors.TextboxBanner.NotFound(query.Id);
        }
        
        return textboxBanner;
    }
}