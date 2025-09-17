namespace Lukki.Contracts.Products;

public record PagedProductsResponse(
    List<PagedProductItemResponse> Products,
    int CurrentPage,
    int TotalPages,
    int TotalItems
    );

public record PagedProductItemResponse(
    string Id,
    string Name,
    PagedProductItemMoneyResponse Price
    );
    
    
public record PagedProductItemMoneyResponse(
    decimal Amount,
    string Currency);