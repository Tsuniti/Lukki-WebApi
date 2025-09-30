namespace Lukki.Application.Orders.Common;

public record ConfirmPaymentResult(
    string Status,
    decimal Amount,
    string Currency,
    string PaymentIntentId
    );