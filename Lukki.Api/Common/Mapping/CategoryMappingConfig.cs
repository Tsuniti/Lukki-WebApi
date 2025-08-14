using Lukki.Application.Categories.Commands.CreateCategory;
using Lukki.Contracts.Categories;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Mapster;
using Lukki.Api.Common.Mapping.Services;

namespace Lukki.Api.Common.Mapping;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>();

        config.NewConfig<Category, CategoryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        TypeAdapterConfig<CategoryId, Guid>.NewConfig().MapWith(id => id.Value);

        config.NewConfig<List<Category>, List<CategoryResponse>>()
            .MapWith(src => CategoryMappingService.BuildCategoryTree(src));
    }
}