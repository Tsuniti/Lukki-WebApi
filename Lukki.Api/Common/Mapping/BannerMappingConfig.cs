using Lukki.Api.ApiModels.Banners;
using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Contracts.Banners;
using Lukki.Domain.BannerAggregate;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class BannerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<(SlideFormModel Model, Stream ImageStream), SlideCommand>()
            .MapWith(src => new SlideCommand(
                src.ImageStream,
                src.Model.Text ?? string.Empty,
                src.Model.Description ?? string.Empty,
                src.Model.ButtonText ?? string.Empty,
                src.Model.ButtonUrl ?? string.Empty,
                src.Model.SortOrder));


        config.NewConfig<(CreateBannerFormModel Form, List<SlideCommand> Slides), CreateBannerCommand>()
            .MapWith(
                src => new CreateBannerCommand(
                    src.Form.Name,
                    src.Form.Description,
                    src.Slides
                ));

        config.NewConfig<Banner, BannerResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(
                dest => dest.Slides,
                src => src.Slides.Select(
                    slide => new SlideResponse(
                        slide.Image.Url,
                        slide.Text,
                        slide.Description,
                        slide.ButtonText,
                        slide.ButtonUrl,
                        slide.SortOrder)));
        config.NewConfig<List<string>, BannerNamesResponse>()
            .Map(dest => dest.BannerNames, src => src);
    }
}