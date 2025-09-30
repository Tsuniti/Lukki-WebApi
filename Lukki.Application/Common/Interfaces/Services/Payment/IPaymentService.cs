using Lukki.Application.Orders.Common;

namespace Lukki.Application.Common.Interfaces.Services.Payment;

public interface IPaymentService
{
    Task<string> CreatePaymentIntentAsync(decimal amount, string currency);
    Task<ConfirmPaymentResult> ConfirmPaymentAsync(string paymentIntentId, string paymentMethodToken);
}