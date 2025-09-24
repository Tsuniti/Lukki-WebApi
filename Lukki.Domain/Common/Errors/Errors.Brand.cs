using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Brand
    {
        public static Error NotFoundByName(string name) => Error.Validation(
            code: "Brand.NotFoundByName",
            description: "Brand not found with name: " + name);
        
        public static Error NotFoundById(string id) => Error.Validation(
            code: "Brand.NotFoundById",
            description: "Brand not found with id: " + id);
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Brand.DuplicateName",
            description: "Brand already exist with name: " + name);
        public static Error NoBrandsFound() => Error.Validation(
            code: "Brand.NoBrandsFound",
            description: "No Brands found. Probably no brand exists");
    }
}