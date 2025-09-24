namespace Lukki.Contracts.Products;

public record ProductResponse(
    string Id,
    string Name,
    string Description,
//    string TargetGroup,
    double? AverageRating,
    MoneyResponse Price,
    List<CategoryPathResponse> CategoryPath,
    List<PromoCategoriesResponse> PromoCategories,
    BrandResponse Brand,
    ColorResponse Color,
    List<MaterialResponse> Materials,
    List<string> Images,
    List<InStockProductResponse> InStockProducts,
    // List<string> ReviewIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record MaterialResponse(
    string Id,
    string Name);


public record ColorResponse(
    string Id,
    string Name);

public record BrandResponse(
    string Id,
    string Name);

public record PromoCategoriesResponse(
    string Id,
    string Name
);
public record CategoryPathResponse(
    string Id,
    string Name);

public record CategoryResponse(
    string Name,
    CategoryResponse? ChildCategory
);

public record MoneyResponse(
    decimal Amount,
    string Currency);

public record InStockProductResponse(
    uint Quantity,
    string Size);