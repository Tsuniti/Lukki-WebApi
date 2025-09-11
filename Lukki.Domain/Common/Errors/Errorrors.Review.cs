using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Review
    {
        public static Error NotFound(string id) => Error.Conflict(
            code: "Review.NotFound", 
            description: $"Review not found with id: {id}");
        
        public static Error Duplicate(string customerId, string productId) => Error.Conflict(
            code: "Review.Duplicate", 
            description: $"Review already exists for CustomerId: {customerId} and ProductId: {productId}");
    }
}