namespace Lukki.Contracts.ReviewBanners;

public record CreateReviewBannerResponse(
    string Id,
    string Title,
    List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);