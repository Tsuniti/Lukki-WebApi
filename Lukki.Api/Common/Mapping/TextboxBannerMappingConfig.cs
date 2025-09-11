using Lukki.Application.TextboxBanners.Commands.CreateTextboxBanner;
using Lukki.Application.TextboxBanners.Queries.GetTextboxBannerById;
using Lukki.Contracts.TextboxBanners;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.TextboxBannerAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class TextboxBannerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<(CreateTextboxBannerRequest Request, Stream Image), CreateTextboxBannerCommand>()
            .MapWith(
                src => new CreateTextboxBannerCommand(
                    src.Request.Name,
                    src.Request.Text,
                    src.Request.Description,
                    src.Request.Placeholder,
                    src.Request.ButtonText,
                    src.Image
                ));

        config.NewConfig<GetTextboxBannerRequest, GetTextboxBannerByIdQuery>()
            .Map(dest => dest, src => src);
        
        config.NewConfig<TextboxBanner, TextboxBannerResponse>()
            .Map(dest => dest, src => src);
        
        TypeAdapterConfig<TextboxBannerId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<Image, string>.NewConfig().MapWith(image => image.Url);

    }
}