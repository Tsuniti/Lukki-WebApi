using Lukki.Domain.Common.Models;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.SellerAggregate.ObjectValues;

namespace Lukki.Domain.SellerAggregate;

public sealed class Seller : AggregateRoot<SellerId>
{
    private readonly List<ProductId> _productIds = new();
    
    public string BrandName { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Seller(
        SellerId sellerId,
        string brandName,
        string? firstName,
        string? lastName,
        string email,
        string passwordHash,
        DateTime createdAt
    ) : base(sellerId)
    {
        BrandName = brandName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
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
            SellerId.CreateUnique(),
            brandName,
            firstName,
            lastName,
            email,
            passwordHash,
            DateTime.UtcNow
        );
    }
}