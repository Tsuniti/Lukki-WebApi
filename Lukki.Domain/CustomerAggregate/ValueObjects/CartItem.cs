using Lukki.Domain.Common.Models;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate.ValueObjects;

public class CartItem : ValueObject
{

    public ProductId ProductId { get; private set; }
    public string Size { get; private set; }
    public uint Quantity { get; private set; }
    
    
    private CartItem(ProductId productId, string size, uint quantity)
    {
        ProductId = productId;
        Size = size;
        Quantity = quantity;
    }

    public static CartItem Create(ProductId productId, string size, uint quantity)
    {
        return new CartItem(productId, size, quantity);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return Size;
    }
}