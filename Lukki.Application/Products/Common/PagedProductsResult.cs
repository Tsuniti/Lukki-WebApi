namespace Lukki.Application.Products.Common;

public record PagedProductsResult(
    List<PagedProductItemResult> Products,
    int CurrentPage,
    int TotalPages,
    int TotalItems
);

public record PagedProductItemResult(
    string Id,
    string Name,
    PagedProductItemMoneyResult Price
);
    
    
public record PagedProductItemMoneyResult(
    decimal Amount,
    string Currency);