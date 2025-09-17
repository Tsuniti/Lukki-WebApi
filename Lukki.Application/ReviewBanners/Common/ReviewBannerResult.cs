using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;

namespace Lukki.Application.ReviewBanners.Common;

public record ReviewBannerResult
(
    ReviewBannerId Id,
    string Title,
    List<ReviewBannerItem> Reviews
    );

public record ReviewBannerItem
(
    Review Review,
    string CustomerName,
    string ProductName,
    string ProductImageUrl,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );