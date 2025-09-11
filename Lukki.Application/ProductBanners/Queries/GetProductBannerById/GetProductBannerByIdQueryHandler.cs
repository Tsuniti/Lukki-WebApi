using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductBannerAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.ProductBanners.Queries.GetProductBannerById;

public class GetProductBannerByIdQueryHandler : 
    IRequestHandler<GetProductBannerByIdQuery, ErrorOr<ProductBanner>>
{
    
    private readonly IProductBannerRepository _productBannerRepository;


    public GetProductBannerByIdQueryHandler(IProductBannerRepository productBannerRepository)
    {
        _productBannerRepository = productBannerRepository;
    }

    public async Task<ErrorOr<ProductBanner>> Handle(GetProductBannerByIdQuery query, CancellationToken cancellationToken)
    {
        
        if (await _productBannerRepository.GetByIdAsync(ProductBannerId.Create(query.Id)) is not ProductBanner productBanner)
        {
            return Errors.ProductBanner.NotFound(query.Id);
        }
        
        return productBanner;
    }
}