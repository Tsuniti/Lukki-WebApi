using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Banners.Queries.GetBanner;

public class GetBannerQueryHandler : 
    IRequestHandler<GetBannerQuery, ErrorOr<Banner>>
{
    
    private readonly IBannerRepository _bannerRepository;


    public GetBannerQueryHandler(IBannerRepository bannerRepository)
    {
        _bannerRepository = bannerRepository;
    }

    public async Task<ErrorOr<Banner>> Handle(GetBannerQuery query, CancellationToken cancellationToken)
    {
        
        if (await _bannerRepository.GetByNameAsync(query.Name) is not Banner banner)
        {
            return Errors.Banner.NotFound(query.Name);
        }
        
        return banner;
    }
}