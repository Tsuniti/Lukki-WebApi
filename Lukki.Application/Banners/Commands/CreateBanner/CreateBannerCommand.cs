using ErrorOr;
using Lukki.Domain.BannerAggregate;
using MediatR;

namespace Lukki.Application.Banners.Commands.CreateBanner;

public record CreateBannerCommand(
    string Name,
    string Description,
    List<SlideCommand> Slides
) : IRequest<ErrorOr<Banner>>;

public record SlideCommand(
    Stream ImageStream, 
    string Text,
    string Description,
    string ButtonText,
    string ButtonUrl,
    short SortOrder
);