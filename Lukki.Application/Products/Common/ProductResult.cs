namespace Lukki.Application.Products.Common;

public record ProductResult(
    string Id,
    string Name,
    string Description,
//    string TargetGroup,
    double? AverageRating,
    MoneyResult Price,
    List<CategoryPath> CategoryPath,
    List<PromoCategoriesResult> PromoCategories,
    BrandResult Brand,
    ColorResult Color,
    List<MaterialResult> Materials,
    List<string> Images,
    List<InStockProductResult> InStockProducts,
    // List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record MaterialResult(
    string Id,
    string Name);

public record ColorResult
    (string Id, 
        string Name);

public record BrandResult(
    string Id,
    string Name);

public record PromoCategoriesResult(
    string Id,
    string Name
);

public record CategoryPath(
    string Id,
    string Name);


public record MoneyResult(
    decimal Amount,
    string Currency);

public record InStockProductResult(
    uint Quantity,
    string Size);