namespace Lukki.Contracts.Customers;

public record CustomerRegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string? Password,           // null if registering with Google
    string? PhoneNumber         // Optional for Customer role
    );