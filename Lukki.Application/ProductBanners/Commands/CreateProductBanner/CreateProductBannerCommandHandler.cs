using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ProductBannerAggregate;
using MediatR;

namespace Lukki.Application.ProductBanners.Commands.CreateProductBanner;

public class CreateProductBannerCommandHandler : IRequestHandler<CreateProductBannerCommand, ErrorOr<ProductBanner>>
{

    private readonly IProductBannerRepository _productBannerRepository;


    public CreateProductBannerCommandHandler(IProductBannerRepository productBannerRepository)
    {
        _productBannerRepository = productBannerRepository;
    }

    public async Task<ErrorOr<ProductBanner>> Handle(CreateProductBannerCommand command, CancellationToken cancellationToken)
    {
        
        // Create ProductBanner
        var productBanner = ProductBanner.Create(
            title: command.Title,
            productIds: command.ProductIds
                .Select(ProductId.Create)
                .ToList() 
        );
        
        // Persist ProductBanner
        await _productBannerRepository.AddAsync(productBanner);
        // Return ProductBanner
        return productBanner;
        
        
    }
}