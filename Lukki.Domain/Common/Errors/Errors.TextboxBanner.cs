using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class TextboxBanner
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "TextboxBanner.NotFound",
            description: "TextboxBanner not found with name: " + name);
        
        public static Error NoNamesFound() => Error.Validation(
            code: "TextboxBanner.NamesNotFound",
            description: "No textboxBanner names found. Probably no textboxBanner exists.");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "TextboxBanner.DuplicateName",
            description: "TextboxBanner with name '" + name + "' already exists.");
    }
}