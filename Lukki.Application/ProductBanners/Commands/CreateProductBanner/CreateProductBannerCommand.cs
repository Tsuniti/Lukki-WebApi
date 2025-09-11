using ErrorOr;
using Lukki.Domain.ProductBannerAggregate;
using MediatR;

namespace Lukki.Application.ProductBanners.Commands.CreateProductBanner;

public record CreateProductBannerCommand(
    string Title,
    List<string> ProductIds
) : IRequest<ErrorOr<ProductBanner>>;