using Lukki.Domain.Common.Enums;
using Lukki.Domain.Common.Interfaces;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<UserId, Guid>, IUser
{
    private readonly List<CartItem> _cartItems = new();
    private readonly List<ProductId> _inWishListProductIds = new();
    private readonly List<OrderId> _orderIds = new();
    private readonly List<ReviewId> _reviewIds = new();
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }

    public string? PhoneNumber { get; private set; }
    
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public IReadOnlyList<ProductId> InWishListProductIds => _inWishListProductIds.AsReadOnly();
    public IReadOnlyList<OrderId> OrderIds => _orderIds.AsReadOnly();
    public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Customer(
        UserId customerId,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string? phoneNumber,
        UserRole role,
        DateTime createdAt
    ) : base(customerId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        Role = role;
        CreatedAt = createdAt;
    }
    
    public static Customer Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string? phoneNumber
    )
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            passwordHash,
            phoneNumber,
            UserRole.CUSTOMER,
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