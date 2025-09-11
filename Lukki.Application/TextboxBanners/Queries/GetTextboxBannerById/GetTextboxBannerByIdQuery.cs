using ErrorOr;
using Lukki.Domain.TextboxBannerAggregate;
using MediatR;

namespace Lukki.Application.TextboxBanners.Queries.GetTextboxBannerById;

public record GetTextboxBannerByIdQuery(
    string Id
    ) : IRequest<ErrorOr<TextboxBanner>>;