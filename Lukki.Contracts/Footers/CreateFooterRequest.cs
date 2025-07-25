namespace Lukki.Contracts.Footers;

public record CreateFooterRequest(
    string Name,
    string CopyrigtText,
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
//Icon does not fall into contracts, because This is IFormFile
    Int16 SortOrder
);