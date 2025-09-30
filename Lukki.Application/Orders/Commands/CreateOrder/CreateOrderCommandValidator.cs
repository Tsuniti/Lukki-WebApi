using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{

    public CreateOrderCommandValidator(IOrderRepository orderRepository)
    {
        
        RuleFor(x => x.BillingAddress)
            .NotEmpty();
        RuleFor(x => x.ShippingAddress)
            .NotEmpty();
        RuleFor(x => x.InOrderProducts)
            .NotEmpty();
    }
}