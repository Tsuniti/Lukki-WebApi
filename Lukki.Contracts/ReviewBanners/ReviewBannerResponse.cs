using Lukki.Contracts.Reviews;

namespace Lukki.Contracts.ReviewBanners;

public record ReviewBannerResponse
(
    string Id,
    string Title,
    List<ReviewBannerItemResponse> Reviews
);

public record ReviewBannerItemResponse
(
    string Id,
    short Rating,
    string Comment,
    string ProductId,
    string CustomerId,
    string CustomerName,
    string ProductName,
    string ProductImageUrl,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);