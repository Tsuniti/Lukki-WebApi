using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Footer
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "Footer.NotFound",
            description: "Footer not found with name: " + name);
        
        public static Error NoNamesFound() => Error.Validation(
            code: "Footer.NamesNotFound",
            description: "No footer names found. Probably no footer exists.");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Footer.DuplicateName",
            description: "Footer with name '" + name + "' already exists.");
        
    }
}