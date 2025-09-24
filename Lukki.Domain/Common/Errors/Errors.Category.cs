using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NotFoundByName(string name) => Error.Validation(
            code: "Category.NotFound",
            description: "Category not found with name: " + name);
        
        public static Error NotFoundById(string id) => Error.Validation(
            code: "Category.NotFound",
            description: "Category not found with name: " + id);
        
        public static Error RootParentNotFound(string id) => Error.Validation(
            code: "Category.RootParentNotFound",
            description: "Root Parent Category not found from category id: " + id);
        
        public static Error NoCategoriesFound() => Error.Validation(
            code: "Category.NoCategoriesFound",
            description: "No Categories found. Probably no category exists");
        
        public static Error NoNamesFound() => Error.Validation(
            code: "Category.NamesNotFound",
            description: "No category names found. Probably no category exists.");
    }
}