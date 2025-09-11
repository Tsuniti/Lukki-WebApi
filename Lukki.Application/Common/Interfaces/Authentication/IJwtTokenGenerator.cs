using Lukki.Domain.CustomerAggregate;

namespace Lukki.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer user);
}