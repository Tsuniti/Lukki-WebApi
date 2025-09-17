using ErrorOr;
using Lukki.Application.Products.Common;
using Lukki.Domain.ProductAggregate;
using MediatR;

namespace Lukki.Application.Products.Queries.GetPagedProducts;

public record GetPagedProductsQuery(
    string? SearchTerm,
    int? MinPrice,
    int? MaxPrice,
    string Currency,
    List<string>? CategoryIds,
    List<string>? PromoCategoryIds,
    List<string>? BrandIds,
    List<string>? ColorIds,
    List<string>? MaterialIds,
    int PageNumber = 1,
    int ItemsPerPage = 31,
    string SortBy = "relevance" // "relevance", "best_selling", "price_asc", "price_desc", "newest"
    ) : IRequest<ErrorOr<PagedProductsResult>>;