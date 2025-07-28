using Lukki.Domain.FooterAggregate;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Footers.Commands.CreateFooter;

public record CreateFooterCommand(
    string Name,
    string CopyrightText,
    List<FooterSectionCommand> Sections
) : IRequest<ErrorOr<Footer>>;

public record FooterSectionCommand(
    string Name,
    List<FooterLinkCommand> Links,
    Int16 SortOrder
);

public record FooterLinkCommand(
    string Text,
    string Url,    
    Stream? IconStream,
    Int16 SortOrder
);