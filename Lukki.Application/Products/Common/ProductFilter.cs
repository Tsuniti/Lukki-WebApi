using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;

namespace Lukki.Application.Products.Common;

public record ProductFilter(
    string? SearchTerm,
    int? MinPrice,
    int? MaxPrice,
    string Currency,
    List<CategoryId>? CategoryIds,
    List<PromoCategoryId>? PromoCategoryIds,
    List<BrandId>? BrandIds,
    List<ColorId>? ColorIds,
    List<MaterialId>? MaterialIds,
    int PageNumber = 1,
    int ItemsPerPage = 31,
    string SortBy = "relevance" // "relevance", "best_selling", "price_asc", "price_desc", "newest"
    );