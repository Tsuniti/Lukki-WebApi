using ErrorOr;
using Lukki.Application.ReviewBanners.Common;
using MediatR;

namespace Lukki.Application.ReviewBanners.Queries.GetReviewBannerById;

public record GetReviewBannerByIdQuery(
    string Id
    ) : IRequest<ErrorOr<ReviewBannerResult>>;