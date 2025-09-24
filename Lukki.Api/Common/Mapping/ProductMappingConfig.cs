using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Contracts.Products;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Mapster;


namespace Lukki.Api.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, CreateProductCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<Product, CreateProductResponse>()
            .Map(dest => dest.AverageRating, src => src.AverageRating.NumRatings > 0 ? src.AverageRating.Value : 0)
            .Map(dest => dest.Price.Amount, src => Math.Round(src.Price.Amount, 2));
        
        TypeAdapterConfig<ProductId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<CategoryId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<Image, string>.NewConfig().MapWith(id => id.Url.ToString());

        /*config.NewConfig<Price, PriceResponse>()
            .Map(dest => dest, src => src);

        config.NewConfig<InStockProduct, PriceResponse>()
            .Map(dest => dest, src => src);*/
    }
}