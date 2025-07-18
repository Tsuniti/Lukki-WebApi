using ErrorOr;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail", 
            description: "Email already in use.");
    }
    
    public static class Product
    {
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "Product not found");

        public static Error NotFoundByIds(IEnumerable<ProductId> missingIds) => Error.NotFound(
            code: "Product.NotFoundByIds",
            description: $"Products not found: {FormatMissingIds(missingIds)}");

        private static string FormatMissingIds(IEnumerable<ProductId> ids) 
            => string.Join(", ", ids.Select(id => id.Value));
    }
}