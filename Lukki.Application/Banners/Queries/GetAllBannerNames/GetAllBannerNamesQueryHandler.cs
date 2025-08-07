using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Banners.Queries.GetAllBannerNames;

public class GetAllBannerNamesQueryHandler : 
    IRequestHandler<GetAllBannerNamesQuery, ErrorOr<List<string>>>
{
    
    private readonly IBannerRepository _bannerRepository;


    public GetAllBannerNamesQueryHandler(IBannerRepository bannerRepository)
    {
        _bannerRepository = bannerRepository;
    }

    public async Task<ErrorOr<List<string>>> Handle(GetAllBannerNamesQuery request, CancellationToken cancellationToken)
    {
        var banners = await _bannerRepository.GetAllNamesAsync();

        if (banners is null || banners.Count == 0)
        {
            return Errors.Banner.NotFound("All banners");
        }

        return banners;
    }
}