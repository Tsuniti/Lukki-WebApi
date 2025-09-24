using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class PromoCategory
    {
        public static Error NotFoundByName(string name) => Error.Validation(
            code: "PromoCategory.NotFoundByName",
            description: "PromoCategory not found with name: " + name);

        public static Error OneOrMoreNotFoundById(List<string> searchIds, List<string> foundIds) => Error.Validation(
                code: "PromoCategory.OneOrMoreNotFoundById",
                description: "One or more promo categories not found by id\n" +
                             $"NotFoundIds: {string.Join(", ", searchIds.Except(foundIds))}");

        

        public static Error DuplicateName(string name) => Error.Validation(
            code: "PromoCategory.DuplicateName",
            description: "PromoCategory already exist with name: " + name);
        public static Error NoPromoCategoriesFound() => Error.Validation(
            code: "PromoCategory.NoPromoCategoriesFound",
            description: "No PromoCategories found. Probably no promoCategory exists");
    }
}