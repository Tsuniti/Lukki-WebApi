namespace Lukki.Contracts.Customers;

public record CustomerLoginRequest(
    string Email,
    string Password);