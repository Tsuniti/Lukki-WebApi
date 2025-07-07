using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Contracts.Products;
using Lukki.Domain.ProductAggregate;
using Mapster;
using InStockProduct = Lukki.Domain.ProductAggregate.ValueObjects.InStockProduct;


namespace Lukki.Api.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateProductRequest Request, string SellerId), CreateProductCommand>()
            .Map(dest => dest.SellerId, src => src.SellerId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Product, ProductResponse>()
             .Map(dest => dest.Id, src => src.Id.Value)
                         .Map(dest => dest.TargetGroup, src => src.TargetGroup.ToString())
                         .Map(dest => dest.AverageRating, src => src.AverageRating.NumRatings > 0 ? src.AverageRating.Value : 0)
                         .Map(dest => dest.ImageUrls, src => src.Images.Select(image => image.Url))
                         .Map(dest => dest.ReviewIds, src => src.ReviewIds.Select(reviewId => reviewId.Value))
                         .Map(dest => dest.CategoryIds, src => src.CategoryIds.Select(categoryId => categoryId.Value))
        ;
        /*config.NewConfig<Price, PriceResponse>()
            .Map(dest => dest, src => src);

        config.NewConfig<InStockProduct, PriceResponse>()
            .Map(dest => dest, src => src);*/

    }
}