using ErrorOr;

namespace Lukki.Domain.Common.Errors;

public static partial class Errors
{
    public static class Order
    {
        
        public static Error NotFound(string id) => Error.Conflict(
            code: "Order.NotFound", 
            description: $"Order not found with id: {id}");
        
        public static Error HasCustomer(string id) => Error.Conflict(
            code: "Order.HasCustomer", 
            description: $"Order with id: {id} has a customer");
        public static Error InvalidCustomer(string id) => Error.Conflict(
            code: "Order.InvalidCustomer", 
            description: $"Order with id: {id} has a different customer");
        
        public static Error InvalidPaymentIntentClientSecret => Error.Conflict(
            code: "Order.InvalidPaymentIntentClientSecret", 
            description: $"The provided Payment Intent Client Secret is invalid");
        
        public static Error FailedToConfirmPayment => Error.Failure(
            code: "Order.FailedToConfirmPayment", 
            description: $"Failed to confirm payment");
        
    }
    
}