using Lukki.Domain.Common.Models;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId>
{
    private readonly List<CartItem> _cartItems = new();
    private readonly List<ProductId> _inWishListProductIds = new();
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? PasswordHash { get; private set; }

    public string? PhoneNumber { get; private set; }
    
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public IReadOnlyList<ProductId> InWishListProductIds => _inWishListProductIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Customer(
        CustomerId customerId,
        string firstName,
        string lastName,
        string email,
        string? passwordHash,
        string? phoneNumber,
        DateTime createdAt
    ) : base(customerId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        CreatedAt = createdAt;
    }
    
    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        string? passwordHash,
        string? phoneNumber
    )
    {
        return new(
            CustomerId.CreateUnique(),
            firstName,
            lastName,
            email,
            passwordHash,
            phoneNumber,
            DateTime.UtcNow
        );
    }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Customer()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
    
}