namespace Lukki.Contracts.Orders;

public record ConfirmPaymentRequest(
    string PaymentIntentId,
    string PaymentMethodToken
    );