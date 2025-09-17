using Lukki.Application.Brands.Commands.CreateBrand;
using Lukki.Contracts.Brands;
using Lukki.Domain.BrandAggregate;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Mapster;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Api.Common.Mapping;

public class BrandMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBrandRequest, CreateBrandCommand>();

        config.NewConfig<Brand, BrandResponse>();
        
        TypeAdapterConfig<BrandId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<Image, string>.NewConfig().MapWith(image => image.Url);
    }
}