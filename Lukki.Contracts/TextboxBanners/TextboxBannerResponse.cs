namespace Lukki.Contracts.TextboxBanners;

public record TextboxBannerResponse
(
    string Id,
    string Name,
    string Text,
    string Description,
    string Placeholder,
    string ButtonText,
    string Background,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);