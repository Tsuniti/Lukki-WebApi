using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.Common.Interfaces;

public interface IUser
{
    UserId Id { get; }
    string Email { get; }
    string PasswordHash { get; }
    string Role { get; }
}