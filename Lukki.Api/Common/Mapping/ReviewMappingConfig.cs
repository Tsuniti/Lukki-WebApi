using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Application.Reviews.Commands.Common;
using Lukki.Application.Reviews.Commands.CreateReview;
using Lukki.Contracts.Products;
using Lukki.Contracts.Reviews;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Mapster;


namespace Lukki.Api.Common.Mapping;

public class ReviewMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateReviewRequest, CreateReviewCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<ReviewResult, ReviewResponse>()
            .Map(dest => dest, src => src.Review)
            .Map(dest => dest.CustomerName, src => src.CustomerName);
        
        TypeAdapterConfig<ReviewId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<ProductId, string>.NewConfig().MapWith(id => id.Value.ToString());

    }
}