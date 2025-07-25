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

        config.NewConfig<(SlideFormModel Form, Stream Image), SlideCommand>()
            .Ignore(dest => dest.ImageStream)
            .Map(dest => dest, src => src.Form)
            .Map(dest => dest.ImageStream, src => src.Image);


        config.NewConfig<(CreateBannerFormModel Form, List<SlideCommand> Slides), CreateBannerCommand>()
            .MapWith(
                src => new CreateBannerCommand(
                    src.Form.Name,
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
                        slide.ButtonText,
                        slide.ButtonUrl,
                        slide.SortOrder)));
    }
}