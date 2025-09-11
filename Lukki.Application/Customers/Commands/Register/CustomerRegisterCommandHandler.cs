using ErrorOr;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Customers.Common;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.CustomerAggregate;
using MediatR;

namespace Lukki.Application.Customers.Commands.Register;

public class CustomerRegisterCommandHandler : 
    IRequestHandler<CustomerRegisterCommand, ErrorOr<CustomerAuthenticationResult>>
{
    
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;

    public CustomerRegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, ICustomerRepository customerRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<CustomerAuthenticationResult>> Handle(CustomerRegisterCommand command, CancellationToken cancellationToken)
    {
        
        // 1. Validate the user doesn't exist
        if (await _customerRepository.GetByEmailAsync(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // 2. Create user (generate unique ID) & Persist to DB
        Customer customer = Customer.Create(
            firstName: command.FirstName,
            lastName: command.LastName,
            passwordHash: command.Password,
            email: command.Email,
            phoneNumber: command.PhoneNumber);
            
        await _customerRepository.AddAsync(customer);
        
        // 3. Create JWT token

        var token = _jwtTokenGenerator.GenerateToken(customer);

        return new CustomerAuthenticationResult(
            customer,
            token);
    }
}