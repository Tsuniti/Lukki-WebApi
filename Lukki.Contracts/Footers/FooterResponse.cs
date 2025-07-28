namespace Lukki.Contracts.Footers;

public record FooterResponse(
    string Id,
    string Name,
    string CopyrightText,
    List<FooterSectionResponse> Sections,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record FooterSectionResponse(
    string Name,
    List<FooterLinkResponse> Links,
    Int16 SortOrder
);

public record FooterLinkResponse(
    string Text,
    string Url,    
    string Icon,
    Int16 SortOrder
);