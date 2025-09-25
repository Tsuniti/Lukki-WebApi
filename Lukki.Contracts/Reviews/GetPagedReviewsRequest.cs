namespace Lukki.Contracts.Reviews;

public record GetPagedReviewsRequest(
    string ProductId,
    int PageNumber = 1,
    int ItemsPerPage = 31,
    string SortBy = "newest" // "newest", "rate_asc", "rate_desc"
    );