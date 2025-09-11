using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "Customer.DuplicateEmail", 
            description: "Email already in use.");
        
        public static Error NotFound(string id) => Error.Conflict(
            code: "Customer.NotFound", 
            description: $"Customer not found with id: {id}");
    }
}