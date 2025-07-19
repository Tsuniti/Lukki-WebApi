using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Contracts.Banners;
using Lukki.Domain.BannerAggregate;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class BannerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateBannerRequest Request, Stream Image), CreateBannerCommand>()
            .Map(dest => dest, src => src.Request)
            .MapWith(
                src => new CreateBannerCommand(
                    src.Request.Name,
                    new SlideCommand(
                        src.Image, // Stream
                        src.Request.Slide.Text ?? "",
                        src.Request.Slide.ButtonText,
                        src.Request.Slide.ButtonUrl,
                        src.Request.Slide.SortOrder
                    )
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