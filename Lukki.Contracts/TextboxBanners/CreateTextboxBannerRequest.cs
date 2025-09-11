namespace Lukki.Contracts.TextboxBanners;

public record CreateTextboxBannerRequest(
    string Name,
    string Text,
    string Description,
    string Placeholder,
    string ButtonText
);