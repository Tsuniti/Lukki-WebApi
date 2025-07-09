namespace Lukki.Contracts.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    string TargetGroup,
    Price Price,
    string CategoryId,
    List<string> Images,
    List<InStockProduct> InStockProducts
);


public record Price(
    decimal Amount,
    string Currency);

public record InStockProduct(
    uint Quantity,
    string Size);
    