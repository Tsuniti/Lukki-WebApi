using ErrorOr;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound(string id) => Error.NotFound(
            code: "Product.NotFound",
            description: "Product not found with id: " + id);

        public static Error NotFoundByIds(IEnumerable<ProductId> missingIds) => Error.NotFound(
            code: "Product.NotFoundByIds",
            description: $"Products not found: {FormatMissingIds(missingIds)}");

        private static string FormatMissingIds(IEnumerable<ProductId> ids) 
            => string.Join(", ", ids.Select(id => id.Value));
        
        public static Error ExchangeRateFailed(string currency) => Error.Failure(
            code: "Product.ExchangeRateFailed",
            description: $"Failed to convert prices to currency: {currency}");
    }
}