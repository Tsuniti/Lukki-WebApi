using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate.Entities;
using Lukki.Domain.OrderAggregate.Enums;
using Lukki.Domain.OrderAggregate.ValueObjects;

namespace Lukki.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<InOrderProduct> _inOrderProducts = new();

    public OrderStatus Status { get; }
    public Price TotalAmount { get; }
    public IReadOnlyList<InOrderProduct> InOrderProducts => _inOrderProducts.AsReadOnly();
    public Adress ShippingAddress { get; }
    public Adress BillingAddress { get; }
    public UserId CustomerId { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Order(
        OrderId orderId,
        OrderStatus status,
        Price totalAmount,
        Adress shippingAddress,
        Adress billingAddress,
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
        Adress shippingAddress,
        Adress billingAddress,
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
}