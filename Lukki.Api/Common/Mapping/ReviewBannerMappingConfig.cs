using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Application.ReviewBanners.Commands.CreateReviewBanner;
using Lukki.Application.ReviewBanners.Common;
using Lukki.Application.Reviews.Commands.CreateReview;
using Lukki.Application.Reviews.Common;
using Lukki.Contracts.Products;
using Lukki.Contracts.ReviewBanners;
using Lukki.Contracts.Reviews;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;
using Mapster;


namespace Lukki.Api.Common.Mapping;

public class ReviewBannerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateReviewBannerRequest, CreateReviewBannerCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<ReviewBannerResult, ReviewBannerResponse>()
            .Map(dest => dest, src => src.Reviews);


        config.NewConfig<ReviewBannerItem, ReviewBannerItemResponse>()
            .Map(dest => dest, src => src.Review);
        
        
        TypeAdapterConfig<ReviewId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<ProductId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<ReviewBannerId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<CustomerId, string>.NewConfig().MapWith(id => id.Value.ToString());
    }
}