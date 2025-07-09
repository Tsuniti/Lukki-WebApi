using Lukki.Domain.Common.Interfaces;

namespace Lukki.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user);
}