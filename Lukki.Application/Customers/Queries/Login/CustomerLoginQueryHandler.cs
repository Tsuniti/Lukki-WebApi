using ErrorOr;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Customers.Common;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.CustomerAggregate;
using MediatR;

namespace Lukki.Application.Customers.Queries.Login;

public class CustomerLoginQueryHandler : 
    IRequestHandler<CustomerLoginQuery, ErrorOr<CustomerAuthenticationResult>>
{
    
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;

    public CustomerLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, ICustomerRepository customerRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<CustomerAuthenticationResult>> Handle(CustomerLoginQuery query, CancellationToken cancellationToken)
    {
        
        // 1. Validate the user exists
        if (await _customerRepository.GetByEmailAsync(query.Email) is not Customer customer)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        // 2. Validate the password is correct
        if (customer.PasswordHash != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(customer);
        
        return new CustomerAuthenticationResult(
            customer,
            token);
    }
}