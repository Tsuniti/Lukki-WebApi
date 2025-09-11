using Lukki.Api.Common.Mapping.Services;
using Lukki.Contracts.ProductBanners;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductBannerAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class ProductBannerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config
            .NewConfig<(ProductBanner Banner, List<Product> Products, List<Category> Categories),
                ProductBannerResponse>()
            .Map(dest => dest.Id, src => src.Banner.Id)
            .Map(dest => dest.Title, src => src.Banner.Title)
            .Map(dest => dest.GroupedProducts, src => ProductBannerMappingService.GroupProductsByCategories(src.Products, src.Categories));
        
        config.NewConfig<ProductBanner, ProductBannerResponse>()
            .Map(dest => dest, src => src);
            
        
        TypeAdapterConfig<ProductBannerId, string>.NewConfig().MapWith(id => id.Value.ToString());

    }
}