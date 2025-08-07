using Lukki.Api.ApiModels.Banners;
using Lukki.Api.ApiModels.CreateFooterFormModel;
using Lukki.Application.Banners.Commands.CreateBanner;
using Lukki.Application.Footers.Commands.CreateFooter;
using Lukki.Contracts.Banners;
using Lukki.Contracts.Footers;
using Lukki.Domain.BannerAggregate;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.FooterAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class FooterMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(FooterLinkFormModel Link, Stream? IconStream), FooterLinkCommand>()
            .MapWith(
                scr => new FooterLinkCommand(
                    scr.Link.Text,
                    scr.Link.Url,
                    scr.IconStream,
                    scr.Link.SortOrder));
        
        config.NewConfig<(FooterSectionFormModel Section, List<FooterLinkCommand> Links), FooterSectionCommand>()
            .MapWith(src => new FooterSectionCommand(
                src.Section.Name,
                src.Links, 
                src.Section.SortOrder));


        config.NewConfig<(CreateFooterFormModel Footer, List<FooterSectionCommand> Sections), CreateFooterCommand>()
            .MapWith(src => new CreateFooterCommand(
                    src.Footer.Name,
                    src.Footer.Name,
                    src.Sections));

        config.NewConfig<Footer, FooterResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(
                dest => dest.Sections,
                src => src.Sections
                    .Select(
                        section => new FooterSectionResponse(
                            section.Name,
                            section.Links.Select(
                                link => new FooterLinkResponse(
                                    link.Text,
                                    link.Url,
                                    link.Icon.Url,
                                    link.SortOrder)).ToList(),
                            section.SortOrder)));

        config.NewConfig<List<string>, FooterNamesResponse>()
            .Map(dest => dest.FooterNames, src => src);
    }
    
}