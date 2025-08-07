using ErrorOr;
using Lukki.Domain.BannerAggregate;
using MediatR;

namespace Lukki.Application.Banners.Queries.GetBannerByName;

public record GetBannerByNameQuery(
    string Name) : IRequest<ErrorOr<Banner>>;