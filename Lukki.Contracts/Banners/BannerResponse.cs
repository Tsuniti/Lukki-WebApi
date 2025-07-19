namespace Lukki.Contracts.Banners;

public record BannerResponse
(
    string Id,
    string Name,
    List<SlideResponse> Slides,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record SlideResponse (
    string Image,
    string Text,
    string ButtonText,
    string ButtonUrl,
    Int16 SortOrder
);