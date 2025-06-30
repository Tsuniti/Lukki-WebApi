using ErrorOr;
using Lukki.Application.Authentication.Common;
using MediatR;

namespace Lukki.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;