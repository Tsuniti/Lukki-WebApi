using Lukki.Domain.Common.Enums;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.Common.Interfaces;

public interface IUser
{
    AggregateRootId<Guid> Id { get; }
    string Email { get; }
    string PasswordHash { get; }
    UserRole Role { get; }
}