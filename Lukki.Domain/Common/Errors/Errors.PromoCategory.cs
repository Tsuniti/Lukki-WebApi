using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class PromoCategory
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "PromoCategory.NotFound",
            description: "PromoCategory not found with name: " + name);
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "PromoCategory.DuplicateName",
            description: "PromoCategory already exist with name: " + name);
        public static Error NoPromoCategoriesFound() => Error.Validation(
            code: "PromoCategory.NoPromoCategoriesFound",
            description: "No PromoCategories found. Probably no promoCategory exists");
    }
}