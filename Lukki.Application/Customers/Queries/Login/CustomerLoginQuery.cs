using ErrorOr;
using Lukki.Application.Customers.Common;
using MediatR;

namespace Lukki.Application.Customers.Queries.Login;

public record CustomerLoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<CustomerAuthenticationResult>>;