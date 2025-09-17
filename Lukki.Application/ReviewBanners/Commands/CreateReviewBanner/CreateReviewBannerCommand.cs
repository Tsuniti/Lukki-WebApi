using ErrorOr;
using Lukki.Domain.ReviewBannerAggregate;
using MediatR;

namespace Lukki.Application.ReviewBanners.Commands.CreateReviewBanner;

public record CreateReviewBannerCommand(
    string Title,
    List<string> ReviewIds
) : IRequest<ErrorOr<ReviewBanner>>;