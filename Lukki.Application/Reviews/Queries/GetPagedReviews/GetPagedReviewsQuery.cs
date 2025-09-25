using ErrorOr;
using Lukki.Application.Reviews.Common;
using MediatR;

namespace Lukki.Application.Reviews.Queries.GetPagedReviews;

public record GetPagedReviewsQuery(
    string ProductId,
    int PageNumber = 1,
    int ItemsPerPage = 31,
    string SortBy = "newest" // "newest", "rate_asc", "rate_desc"
    ) : IRequest<ErrorOr<PagedReviewsResult>>;