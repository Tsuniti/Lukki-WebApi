using Lukki.Domain.Common.Interfaces;

namespace Lukki.Application.Authentication.Common;

public record AuthenticationResult 
(
    IUser User,
    string Token
);