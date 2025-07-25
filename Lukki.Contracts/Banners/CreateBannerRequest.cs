namespace Lukki.Contracts.Banners;

public record CreateBannerRequest(
    string Name,
    List<SlideRequest> Slides
);

public record SlideRequest(
    string? Text,
    string ButtonText,
    string ButtonUrl,
    Int16 SortOrder
);