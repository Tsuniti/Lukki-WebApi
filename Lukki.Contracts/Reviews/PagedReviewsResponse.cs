namespace Lukki.Contracts.Reviews;

public record PagedReviewsResponse(
    List<ReviewResponse> Reviews,
    int CurrentPage,
    int TotalPages,
    int TotalItems
    );