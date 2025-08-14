using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Contracts.Products;
using Lukki.Domain.ProductAggregate;
using Mapster;


namespace Lukki.Api.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, CreateProductCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            // .Map(dest => dest.TargetGroup, src => src.TargetGroup.ToString())
            .Map(dest => dest.AverageRating, src => src.AverageRating.NumRatings > 0 ? src.AverageRating.Value : 0)
            .Map(dest => dest.CategoryId, src => src.CategoryId.Value)
            .Map(dest => dest.Images, src => src.Images.Select(image => image.Url))
            ;
        /*config.NewConfig<Price, PriceResponse>()
            .Map(dest => dest, src => src);

        config.NewConfig<InStockProduct, PriceResponse>()
            .Map(dest => dest, src => src);*/
    }
}