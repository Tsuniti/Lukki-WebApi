namespace Lukki.Contracts.Products;

public record CreateProductResponse(
    string Id,
    string Name,
    string Description,
//    string TargetGroup,
    double? AverageRating,
    CreateMoneyResponse Price,
    string CategoryId,
    List<string> PromoCategoryIds,
    string BrandId,
    string ColorId,
    List<string> MaterialIds,
    List<string> Images,
    List<CreateInStockProductResponse> InStockProducts,
    // List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateMoneyResponse(
    decimal Amount,
    string Currency);

public record CreateInStockProductResponse(
    uint Quantity,
    string Size);