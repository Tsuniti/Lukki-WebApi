using Google.Apis.Auth;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Customers.Common;
using Lukki.Domain.Common.Errors;
using MediatR;
using ErrorOr;
using Lukki.Domain.CustomerAggregate;

namespace Lukki.Application.Customers.Commands.GoogleRegister;

public class CustomerGoogleAuthCommandHandler 
    : IRequestHandler<CustomerGoogleAuthCommand, ErrorOr<CustomerAuthenticationResult>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public CustomerGoogleAuthCommandHandler(
        ICustomerRepository customerRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _customerRepository = customerRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<CustomerAuthenticationResult>> Handle(
        CustomerGoogleAuthCommand request, 
        CancellationToken cancellationToken)
    {
        
        // 1. Check Google Idtoken
        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
        }
        catch
        {
            return Errors.Customer.InvalidGoogleToken;
        }

        if (payload == null)
            return Errors.Customer.InvalidGoogleToken;

        // 2. Looking for a user by email
        if(await _customerRepository.GetByEmailAsync(payload.Email) is Customer existingCustomer)
        {
            //There is already a user just log them in
            var token = _jwtTokenGenerator.GenerateToken(existingCustomer);
            return new CustomerAuthenticationResult(existingCustomer, token);
        }

        // 3.If not, we create a new user
        
        var newCustomer = Customer.Create(
            firstName: payload.GivenName,
            lastName: payload.FamilyName,
            email: payload.Email,
            passwordHash: null, // No password for Google-registered users
            phoneNumber: null  // Optional
            );

        await _customerRepository.AddAsync(newCustomer);

        // 4. Generate JWT
        var jwt = _jwtTokenGenerator.GenerateToken(newCustomer);

        return new CustomerAuthenticationResult(newCustomer, jwt);
    }
}