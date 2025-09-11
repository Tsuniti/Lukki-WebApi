using FluentValidation;

namespace Lukki.Application.Customers.Queries.Login;

public class CustomerLoginQueryValidator : AbstractValidator<CustomerLoginQuery>
{
    public CustomerLoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
    
}