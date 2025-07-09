using Lukki.Domain.Common.Models;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate.ValueObjects;

public class CartItem : ValueObject
{
    public ProductId ProductId { get; private set; }
    public string Size { get; private set; }
    public uint Quantity { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return Size;
    }
}