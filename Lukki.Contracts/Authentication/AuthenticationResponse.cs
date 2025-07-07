namespace Lukki.Contracts.Authentication;

public record AuthenticationResponse(
    string Id,
    string Email,
    string Role,
    string Token);