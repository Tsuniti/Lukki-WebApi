using ErrorOr;
using Lukki.Domain.HeaderAggregate;
using MediatR;

namespace Lukki.Application.Headers.Commands.CreateHeader;

public record CreateHeaderCommand(
    string Name,
    Stream LogoStream,
    Stream OnHoverLogoStream,
    List<HeaderBurgerMenuLinkCommand> BurgerMenuLinks,
    List<HeaderIconButtonCommand> Buttons
) : IRequest<ErrorOr<MyHeader>>;





public record HeaderBurgerMenuLinkCommand(
    string Text,
    string Url,    
    Int16 SortOrder
);

public record HeaderIconButtonCommand(
    Stream IconStream,
    string Url,
    Int16 SortOrder
    );