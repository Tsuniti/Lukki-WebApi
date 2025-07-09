using Lukki.Domain.Common.Interfaces;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    IUser? GetUserByEmail(string email);
    void Add(IUser user);
}