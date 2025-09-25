namespace Lukki.Application.Reviews.Common;

public record PagedReviewsResult(
    List<ReviewResult> Reviews,
    int CurrentPage,
    int TotalPages,
    int TotalItems
);