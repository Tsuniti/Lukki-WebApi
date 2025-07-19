namespace Lukki.Contracts.Banners;

public record CreateBannerRequest(
    string Name,
    Slide Slide
);

public record Slide(
    string? Text,
    string ButtonText,
    string ButtonUrl,
    Int16 SortOrder
);