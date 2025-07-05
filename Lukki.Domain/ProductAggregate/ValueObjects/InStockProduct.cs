using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public class InStockProduct : ValueObject
{

    public uint Quantity { get; private set; }
    public string Size { get; }
    
    public InStockProduct(uint quantity, string size)
    {
        Quantity = quantity;
        Size = size;
    }
    
    public void UpdateQuantity(uint quantity)
    {
        
        Quantity = quantity;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Size;
    }
}