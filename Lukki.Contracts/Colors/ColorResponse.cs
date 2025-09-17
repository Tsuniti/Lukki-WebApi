namespace Lukki.Contracts.Colors;

public record ColorResponse
(
    string Id,
    string Name,
    string? HexColorCode,
    string? IconUrl
    );