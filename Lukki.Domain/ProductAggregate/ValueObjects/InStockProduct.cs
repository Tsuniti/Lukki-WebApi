using Lukki.Domain.Common.Models;

namespace Lukki.Domain.ProductAggregate.ValueObjects;

public class InStockProduct : ValueObject
{

    public uint Quantity { get; private set; }
    public string Size { get; }
    
    private InStockProduct(uint quantity, string size)
    {
        Quantity = quantity;
        Size = size;
    }
    
    public static InStockProduct Create(uint quantity, string size)
    {

        if (string.IsNullOrWhiteSpace(size))
        {
            throw new ArgumentException("Size cannot be null or empty.", nameof(size));
        }

        return new InStockProduct(quantity, size);
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