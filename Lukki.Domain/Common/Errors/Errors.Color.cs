using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Color
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "Color.NotFound",
            description: "Color not found with name: " + name);
        public static Error NoColorRepresentation() => Error.Validation(
            code: "Color.NoColorRepresentation",
            description: "Color must have one representation (HexColorCode or Image)");
        
        public static Error MultipleColorRepresentations() => Error.Validation(
            code: "Color.MultipleColorRepresentations",
            description: "Color must have ONLY ONE representation (HexColorCode or Image)");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Color.DuplicateName",
            description: "Color already exist with name: " + name);
        
        public static Error NoColorsFound() => Error.Validation(
            code: "Color.NoColorsFound",
            description: "No Colors found. Probably no color exists");
        
        public static Error ImageTooLarge(long yourImageSize, int maxImageSize) => Error.Validation(
            code: "Color.ImageTooLarge",
            description: $"Image is too large. Your image = {yourImageSize} bytes, max image size = {maxImageSize} bytes.");
    }
}