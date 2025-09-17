namespace Lukki.Contracts.Products;

public record GetPagedProductsRequest(
    string? SearchTerm,
    int? MinPrice,
    int? MaxPrice,
    List<string>? CategoryIds,
    List<string>? PromoCategoryIds,
    List<string>? BrandIds,
    List<string>? ColorIds,
    List<string>? MaterialIds,
    string Currency = "USD",
    int PageNumber = 1,
    int ItemsPerPage = 31,
    string SortBy = "relevance" // "relevance", "best_selling", "price_asc", "price_desc", "newest"
    );