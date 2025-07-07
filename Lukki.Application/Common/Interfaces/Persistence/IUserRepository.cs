using Lukki.Domain.Common.Interfaces;
using Lukki.Domain.User;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    IUser? GetUserByEmail(string email);
    void Add(IUser user);
}