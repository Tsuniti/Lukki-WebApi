namespace Lukki.Contracts.Customers;

public record CustomerAuthenticationResponse(
    string Id,
    string Email,
    string Token);