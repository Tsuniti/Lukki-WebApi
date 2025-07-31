using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Footer
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "Footer.NotFound",
            description: "Footer not found with name: " + name);
    }
}