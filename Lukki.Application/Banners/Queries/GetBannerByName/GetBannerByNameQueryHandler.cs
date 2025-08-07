using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Banners.Queries.GetBannerByName;

public class GetBannerByNameQueryHandler : 
    IRequestHandler<GetBannerByNameQuery, ErrorOr<Banner>>
{
    
    private readonly IBannerRepository _bannerRepository;


    public GetBannerByNameQueryHandler(IBannerRepository bannerRepository)
    {
        _bannerRepository = bannerRepository;
    }

    public async Task<ErrorOr<Banner>> Handle(GetBannerByNameQuery byNameQuery, CancellationToken cancellationToken)
    {
        
        if (await _bannerRepository.GetByNameAsync(byNameQuery.Name) is not Banner banner)
        {
            return Errors.Banner.NotFound(byNameQuery.Name);
        }
        
        return banner;
    }
}