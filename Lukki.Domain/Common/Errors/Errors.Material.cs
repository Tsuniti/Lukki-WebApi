using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Material
    {
        public static Error NotFound(string name) => Error.Validation(
            code: "Material.NotFound",
            description: "Material not found with name: " + name);
        
        public static Error OneOrMoreNotFoundById(List<string> searchIds, List<string> foundIds) => Error.Validation(
            code: "Material.OneOrMoreNotFoundById",
            description: "One or more materials not found by id\n" +
                         $"NotFoundIds: {string.Join(", ", searchIds.Except(foundIds))}");
        
        public static Error DuplicateName(string name) => Error.Validation(
            code: "Material.DuplicateName",
            description: "Material already exist with name: " + name);
        public static Error NoMaterialsFound() => Error.Validation(
            code: "Material.NoMaterialsFound",
            description: "No Materials found. Probably no material exists");
    }
}