using Lukki.Domain.User;

namespace Lukki.Application.Authentication.Common;

public record AuthenticationResult 
(
    User User,
    string Token
);