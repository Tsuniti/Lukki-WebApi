namespace Lukki.Contracts.Reviews;

public record ReviewResponse
(
    string Id,
    short Rating,
    string Comment,
    string ProductId,
    string CustomerId,
    string CustomerName,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);