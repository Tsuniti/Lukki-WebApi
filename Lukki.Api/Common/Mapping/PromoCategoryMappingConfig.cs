using Lukki.Application.PromoCategories.Commands.CreatePromoCategory;
using Lukki.Contracts.PromoCategories;
using Lukki.Domain.PromoCategoryAggregate;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class PromoCategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePromoCategoryRequest, CreatePromoCategoryCommand>();

        config.NewConfig<PromoCategory, PromoCategoryResponse>();
        
        TypeAdapterConfig<PromoCategoryId, string>.NewConfig().MapWith(id => id.Value.ToString());
    }
}