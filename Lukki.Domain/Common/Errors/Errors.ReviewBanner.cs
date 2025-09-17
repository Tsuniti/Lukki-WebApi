using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class ReviewBanner
    {
        public static Error NotFound(string id) => Error.Validation(
            code: "ReviewBanner.NotFound",
            description: "ReviewBanner not found with id: " + id);
    }
}