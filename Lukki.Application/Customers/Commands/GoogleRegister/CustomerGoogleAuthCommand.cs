using Lukki.Application.Customers.Common;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Customers.Commands.GoogleRegister;

public record CustomerGoogleAuthCommand
(
    string IdToken
    ) : IRequest<ErrorOr<CustomerAuthenticationResult>>;