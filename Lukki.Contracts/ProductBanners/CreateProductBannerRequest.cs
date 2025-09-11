namespace Lukki.Contracts.ProductBanners;

public record CreateProductBannerRequest(
    string Title,
    List<string> ProductIds
);