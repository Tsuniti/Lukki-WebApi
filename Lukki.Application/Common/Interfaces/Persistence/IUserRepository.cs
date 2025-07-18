using Lukki.Domain.Common.Interfaces;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<IUser?> GetUserByEmailAsync(string email);
    Task AddAsync(IUser user);
}