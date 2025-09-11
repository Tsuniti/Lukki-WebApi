namespace Lukki.Contracts.Banners;

public record CreateBannerRequest(
    string Name,
    string Description,
    List<SlideRequest> Slides
);

public record SlideRequest(
    string? Text,
    string? Description,
    string ButtonText,
    string ButtonUrl,
    Int16 SortOrder
);