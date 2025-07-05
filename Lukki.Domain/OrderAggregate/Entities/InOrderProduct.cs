using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.OrderAggregate.Entities;

public class InOrderProduct : Entity<InOrderProductId>
{
    public Price PriceAtTimeOfOrder { get; }
    public string Size { get; }
    public uint Quantity { get; }
    public ProductId ProductId { get; }

    private InOrderProduct(InOrderProductId inOrderProductId, Price priceAtTimeOfOrder, string size, uint quantity, ProductId productId) : base(inOrderProductId)
    { 

        PriceAtTimeOfOrder = priceAtTimeOfOrder;
        Size = size;
        Quantity = quantity;
        ProductId = productId;
    }

    public static InOrderProduct Create(
        Price priceAtTimeOfOrder,
        string size,
        uint quantity,
        ProductId productId)
    {
        return new InOrderProduct(
            InOrderProductId.CreateUnique(),
            priceAtTimeOfOrder,
            size,
            quantity,
            productId);
    }
}