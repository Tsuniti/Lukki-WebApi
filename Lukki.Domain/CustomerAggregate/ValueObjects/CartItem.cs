using Lukki.Domain.Common.Models;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Domain.CustomerAggregate.ValueObjects;

public class CartItem : ValueObject
{
    public ProductId ProductId { get; }
    public string Size { get; }
    public uint Quantity { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return Size;
    }
}