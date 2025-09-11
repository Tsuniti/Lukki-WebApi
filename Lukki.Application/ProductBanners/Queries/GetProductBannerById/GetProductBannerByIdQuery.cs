using ErrorOr;
using Lukki.Domain.ProductBannerAggregate;
using MediatR;

namespace Lukki.Application.ProductBanners.Queries.GetProductBannerById;

public record GetProductBannerByIdQuery(
    string Id
    ) : IRequest<ErrorOr<ProductBanner>>;