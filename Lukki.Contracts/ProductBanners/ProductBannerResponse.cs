using Lukki.Contracts.Products;

namespace Lukki.Contracts.ProductBanners;

public record ProductBannerResponse
(
    string Id,
    string Title,
    List<GroupedProduct> GroupedProducts,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record GroupedProduct
(
    string GroupName,
    List<ProductResponse> Products);