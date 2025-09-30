namespace Lukki.Contracts.Orders;

public record ConfirmPaymentResponse(
    string Status,
    string PaymentIntentId
    );