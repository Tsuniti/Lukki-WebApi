using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class ProductBanner
    {
        public static Error NotFound(string id) => Error.Validation(
            code: "ProductBanner.NotFound",
            description: "ProductBanner not found with id: " + id);
    }
}