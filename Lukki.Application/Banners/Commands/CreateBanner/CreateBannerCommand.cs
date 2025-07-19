using ErrorOr;
using Lukki.Domain.BannerAggregate;
using MediatR;

namespace Lukki.Application.Banners.Commands.CreateBanner;

public record CreateBannerCommand(
    string Name,
    SlideCommand Slide
) : IRequest<ErrorOr<Banner>>;

public record SlideCommand(
    Stream ImageStream, 
    string Text,
    string ButtonText,
    string ButtonUrl,
    short SortOrder
);