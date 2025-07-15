using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate.Entities;
using Lukki.Domain.OrderAggregate.Enums;
using Lukki.Domain.OrderAggregate.ValueObjects;

namespace Lukki.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<InOrderProduct> _inOrderProducts = new();

    public OrderStatus Status { get; private set; }
    public Price TotalAmount { get; private set; }

    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public UserId? CustomerId { get; private set; } // Nullable to save orders if customer is deleted
    public IReadOnlyList<InOrderProduct> InOrderProducts => _inOrderProducts.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Order(
        OrderId orderId,
        OrderStatus status,
        Price totalAmount,
        Address shippingAddress,
        Address billingAddress,
        UserId customerId,
        DateTime createdAt
    ) : base(orderId)
    {
        Status = status;
        TotalAmount = totalAmount;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        CustomerId = customerId;
        CreatedAt = createdAt;
    }
    
    public static Order Create(
        OrderStatus status,
        Price totalAmount,
        Address shippingAddress,
        Address billingAddress,
        UserId customerId
    )
    {
        return new(
            OrderId.CreateUnique(),
            status,
            totalAmount,
            shippingAddress,
            billingAddress,
            customerId,
            DateTime.UtcNow
        );
    }
    
#pragma warning disable CS8618
    private Order()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}