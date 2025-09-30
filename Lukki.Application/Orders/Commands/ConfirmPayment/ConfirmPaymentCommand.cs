using Lukki.Application.Orders.Common;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Orders.Commands.ConfirmPayment;

public record ConfirmPaymentCommand(
    string PaymentIntentId,
    string PaymentMethodToken
    ) : IRequest<ErrorOr<ConfirmPaymentResult>>;