namespace Lukki.Contracts.Products;

public record ProductResponse(
    string Id,
    string Name,
    string Description,
    string TargetGroup,
    float? AverageRating,
    PriceResponse Price,
    string CategoryId,
    List<string> ImageUrls,
    List<InStockProductResponse> InStockProducts,
    List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record PriceResponse(
    decimal Amount,
    string Currency);

public record InStockProductResponse(
    uint Quantity,
    string Size);