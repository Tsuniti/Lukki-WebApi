using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Header
    {
        
        public static Error NotFound(string name) => Error.Validation(
            code: "Header.NotFound",
            description: "Header not found with name: " + name);
        
        public static Error NoNamesFound() => Error.Validation(
            code: "Header.NamesNotFound",
            description: "No header names found. Probably no header exists.");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Header.DuplicateName",
            description: "Header with name '" + name + "' already exists.");
        public static Error ImageTooLarge(long yourImageSize, int maxImageSize) => Error.Validation(
            code: "Header.ImageTooLarge",
            description: $"Image is too large. Your image = {yourImageSize} bytes, max image size = {maxImageSize} bytes.");
        
    }
}