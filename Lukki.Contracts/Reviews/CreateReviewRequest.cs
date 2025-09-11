namespace Lukki.Contracts.Reviews;

public record CreateReviewRequest
(
    short Rating,
    string Comment,
    string ProductId
    );