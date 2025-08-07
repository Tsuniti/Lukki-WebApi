using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Banner
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "Banner.NotFound",
            description: "Banner not found with name: " + name);
        
        public static Error NoNamesFound() => Error.Validation(
            code: "Banner.NamesNotFound",
            description: "No banner names found. Probably no banner exists.");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Banner.DuplicateName",
            description: "Banner with name '" + name + "' already exists.");
    }
}