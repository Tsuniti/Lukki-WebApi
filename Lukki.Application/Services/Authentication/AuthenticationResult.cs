using Lukki.Domain.Entities;

namespace Lukki.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);