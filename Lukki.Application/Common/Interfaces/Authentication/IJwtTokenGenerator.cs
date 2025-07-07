using Lukki.Domain.Common.Interfaces;
using Lukki.Domain.User;

namespace Lukki.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user);
}