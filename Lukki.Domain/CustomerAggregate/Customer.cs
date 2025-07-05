using Lukki.Domain.Common.Models;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId>
{
    private readonly List<CartItem> _cartItems = new();
    private readonly List<ProductId> _inWishListProductIds = new();
    private readonly List<OrderId> _orderIds = new();
    private readonly List<ReviewId> _reviewIds = new();
    
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public string? PhoneNumber { get; }
    
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public IReadOnlyList<ProductId> InWishListProductIds => _inWishListProductIds.AsReadOnly();
    public IReadOnlyList<OrderId> OrderIds => _orderIds.AsReadOnly();
    public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Customer(
        CustomerId customerId,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
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
        string passwordHash,
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
    
}