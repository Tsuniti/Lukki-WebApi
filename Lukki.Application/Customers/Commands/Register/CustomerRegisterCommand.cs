using ErrorOr;
using Lukki.Application.Customers.Common;
using MediatR;

namespace Lukki.Application.Customers.Commands.Register;

public record CustomerRegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Password,           // null if registering with Google
    string? PhoneNumber         // Optional for Customer role
    ) : IRequest<ErrorOr<CustomerAuthenticationResult>>;