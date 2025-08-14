using Lukki.Api.ApiModels.Footer;
using Lukki.Application.Footers.Commands.CreateFooter;
using Lukki.Contracts.Footers;
using Lukki.Domain.FooterAggregate;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class FooterMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(FooterLinkFormModel Link, Stream? IconStream), FooterLinkCommand>()
            .MapWith(
                scr => new FooterLinkCommand(
                    scr.Link.Text ?? String.Empty,
                    scr.Link.Url,
                    scr.IconStream ?? null,
                    scr.Link.SortOrder));
        
        config.NewConfig<(FooterSectionFormModel Section, List<FooterLinkCommand> Links), FooterSectionCommand>()
            .MapWith(src => new FooterSectionCommand(
                src.Section.Name,
                src.Links, 
                src.Section.SortOrder));


        config.NewConfig<(CreateFooterFormModel Footer, List<FooterSectionCommand> Sections), CreateFooterCommand>()
            .MapWith(src => new CreateFooterCommand(
                    src.Footer.Name,
                    src.Footer.CopyrightText,
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
                                    link.Icon.Url ?? String.Empty,
                                    link.SortOrder)).ToList(),
                            section.SortOrder)));

        config.NewConfig<List<string>, FooterNamesResponse>()
            .Map(dest => dest.FooterNames, src => src);
    }
    
}