using Lukki.Domain.Common.Interfaces;
using Lukki.Domain.User;

namespace Lukki.Application.Authentication.Common;

public record AuthenticationResult 
(
    IUser User,
    string Token
);