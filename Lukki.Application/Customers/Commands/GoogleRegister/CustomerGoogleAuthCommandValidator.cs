using FluentValidation;

namespace Lukki.Application.Customers.Commands.GoogleRegister;

public class CustomerGoogleAuthCommandValidator : AbstractValidator<CustomerGoogleAuthCommand>
{
    public CustomerGoogleAuthCommandValidator()
    {
        RuleFor(x => x.IdToken)
            .NotEmpty();
    }
}