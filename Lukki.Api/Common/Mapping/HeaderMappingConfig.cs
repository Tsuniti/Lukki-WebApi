using Lukki.Api.Common.Mapping.Services;
using Lukki.Application.Headers.Commands.CreateHeader;
using Lukki.Contracts.Categories;
using Lukki.Contracts.Header;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.HeaderAggregate;
using Lukki.Domain.HeaderAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class HeaderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(HeaderIconButtonFormModel Button, Stream? IconStream), HeaderIconButtonCommand>()
            .MapWith(
                src => new HeaderIconButtonCommand(
                    src.IconStream ?? null,
                    src.Button.Url,
                    src.Button.SortOrder));
        


        config.NewConfig<(CreateHeaderFormModel Header, Stream Logo, Stream OnHoverLogo, List<HeaderIconButtonCommand> Buttons), CreateHeaderCommand>()
            .MapWith(src => new CreateHeaderCommand(
                    src.Header.Name,
                    src.Logo,
                    src.OnHoverLogo,
                    src.Header.BurgerMenuLinks
                        .Select(link => new HeaderBurgerMenuLinkCommand(
                            link.Text,
                            link.Url,
                            link.SortOrder))
                        .ToList(),
                    src.Buttons));

        config.NewConfig<(MyHeader Header, List<CategoryResponse> Categories), HeaderResponse>()
            .Map(dest => dest.Id, src => src.Header.Id.Value)
            .Map(dest => dest.Name, src => src.Header.Name)
            .Map(dest => dest.LogoUrl, src => src.Header.Logo.Url)
            .Map(dest => dest.OnHoverLogoUrl, src => src.Header.OnHoverLogo.Url)
            .Map(dest => dest.BurgerMenu, src => new BurgerMenuResponse(
                
                
                src.Categories,
                src.Header.BurgerMenuLinks
                    .Select(link => new LinkResponse(
                        link.Text,
                        link.Url,
                        link.SortOrder))
                    .ToList()
            ))
            .Map(dest => dest.Buttons, src => src.Header.Buttons)
            .Map(dest => dest.CreatedAt, src => src.Header.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.Header.UpdatedAt);

        
        config.NewConfig<BurgerMenuLink, LinkResponse>()
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.SortOrder, src => src.SortOrder);

        config.NewConfig<List<string>, HeaderNamesResponse>()
            .Map(dest => dest.HeaderNames, src => src);
        
        config.NewConfig<Category, CategoryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        TypeAdapterConfig<CategoryId, Guid>.NewConfig().MapWith(id => id.Value);

        config.NewConfig<List<Category>, List<CategoryResponse>>()
            .MapWith(src => CategoryMappingService.BuildCategoryTree(src));
        
    }
    
}