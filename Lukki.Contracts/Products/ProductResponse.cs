namespace Lukki.Contracts.Products;

public record ProductResponse(
    string Id,
    string Name,
    string Description,
//    string TargetGroup,
    float? AverageRating,
    MoneyResponse Price,
    string CategoryId,
    List<string> Images,
    List<InStockProductResponse> InStockProducts,
    List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record MoneyResponse(
    decimal Amount,
    string Currency);

public record InStockProductResponse(
    uint Quantity,
    string Size);