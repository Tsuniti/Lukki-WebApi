using ErrorOr;
using Lukki.Domain.ReviewAggregate.ValueObjects;

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
        
        public static Error NotFoundByIds(IEnumerable<ReviewId> missingIds) => Error.NotFound(
            code: "Product.NotFoundByIds",
            description: $"Products not found: {FormatMissingIds(missingIds)}");
        
        private static string FormatMissingIds(IEnumerable<ReviewId> ids) 
            => string.Join(", ", ids.Select(id => id.Value));
    }
}