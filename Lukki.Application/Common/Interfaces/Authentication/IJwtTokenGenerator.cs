using Lukki.Domain.Entities;

namespace Lukki.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}