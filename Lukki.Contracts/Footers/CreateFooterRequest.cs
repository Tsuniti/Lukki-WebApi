namespace Lukki.Contracts.Footers;

public record CreateFooterRequest(
    string Name,
    string CopyrightText,
    List<FooterSectionRequest> Sections
);

public record FooterSectionRequest(
    string Name,
    List<FooterLinkRequest> Links,
    Int16 SortOrder
);

public record FooterLinkRequest(
    string Text,
    string Url,    
    Int16 SortOrder
);