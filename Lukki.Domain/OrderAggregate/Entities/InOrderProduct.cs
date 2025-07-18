using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.OrderAggregate.Entities;

public class InOrderProduct : Entity<InOrderProductId>
{
    public Money PriceAtTimeOfOrder { get; private set; }
    public string Size { get; private set; }
    public uint Quantity { get; private set; }
    public ProductId ProductId { get; private set; }

    private InOrderProduct(InOrderProductId inOrderProductId, Money priceAtTimeOfOrder, string size, uint quantity, ProductId productId) : base(inOrderProductId)
    { 

        PriceAtTimeOfOrder = priceAtTimeOfOrder;
        Size = size;
        Quantity = quantity;
        ProductId = productId;
    }

    public static InOrderProduct Create(
        Money priceAtTimeOfOrder,
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
    
#pragma warning disable CS8618
    private InOrderProduct()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
    
}