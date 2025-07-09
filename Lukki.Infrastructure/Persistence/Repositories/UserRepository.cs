using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Interfaces;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<IUser> _users = new();
    public void Add(IUser user)
    {
        _users.Add(user);
    }
    public IUser? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }

}