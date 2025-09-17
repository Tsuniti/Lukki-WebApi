namespace Lukki.Contracts.ReviewBanners;

public record CreateReviewBannerRequest(
    string Title,
    List<string> ReviewIds
);