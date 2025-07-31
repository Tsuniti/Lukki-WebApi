using ErrorOr;
using Lukki.Application.Authentication.Common;
using Lukki.Domain.BannerAggregate;
using MediatR;

namespace Lukki.Application.Banners.Queries.GetBanner;

public record GetBannerQuery(
    string Name) : IRequest<ErrorOr<Banner>>;