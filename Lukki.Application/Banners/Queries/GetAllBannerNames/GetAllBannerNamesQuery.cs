using Lukki.Domain.BannerAggregate;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Banners.Queries.GetAllBannerNames;

public record GetAllBannerNamesQuery : IRequest<ErrorOr<List<string>>>;