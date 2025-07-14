namespace Lukki.Contracts.Authentication;

public record RegisterRequest(
    string? BrandName,          // Optional for Seller role
    string? FirstName,          // null if seller don't want to provide it
    string? LastName,           // null if seller don't want to provide it
    string Email,
    string? Password,           // null if registering with Google
    string? PhoneNumber         // Optional for Customer role
    );