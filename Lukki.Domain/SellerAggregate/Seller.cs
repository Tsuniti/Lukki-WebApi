using Lukki.Domain.Common.Enums;
using Lukki.Domain.Common.Interfaces;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.SellerAggregate;

public sealed class Seller : AggregateRoot<UserId, Guid>, IUser
{
    private readonly List<ProductId> _productIds = new();
    
    public string BrandName { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Seller(
        UserId sellerId,
        string brandName,
        string? firstName,
        string? lastName,
        string email,
        string passwordHash,
        string role,
        DateTime createdAt
    ) : base(sellerId)
    {
        BrandName = brandName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
    }
    
    public static Seller Create(
        string brandName,
        string? firstName,
        string? lastName,
        string email,
        string passwordHash
    )
    {
        return new(
            UserId.CreateUnique(),
            brandName,
            firstName,
            lastName,
            email,
            passwordHash,
            UserRole.SELLER.ToString(),
            DateTime.UtcNow
        );
    }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Seller()
    {
        // Required for EF Core
    }
#pragma warning restore CS8618 

}

