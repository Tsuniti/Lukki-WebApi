using ErrorOr;
using Lukki.Domain.TextboxBannerAggregate;
using MediatR;

namespace Lukki.Application.TextboxBanners.Commands.CreateTextboxBanner;

public record CreateTextboxBannerCommand(
    string Name,
    string Text,
    string Description,
    string Placeholder,
    string ButtonText,
    Stream BackgroundImageStream
) : IRequest<ErrorOr<TextboxBanner>>;
