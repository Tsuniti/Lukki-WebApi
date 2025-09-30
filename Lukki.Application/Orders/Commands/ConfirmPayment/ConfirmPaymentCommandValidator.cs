using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Orders.Commands.ConfirmPayment;

public class ConfirmPaymentCommandValidator : AbstractValidator<ConfirmPaymentCommand>
{
    public ConfirmPaymentCommandValidator(IOrderRepository orderRepository)
    {
        RuleFor(x => x.PaymentIntentId)
            .NotEmpty();
        
        RuleFor(x => x.PaymentMethodToken)
            .NotEmpty();
    }
}