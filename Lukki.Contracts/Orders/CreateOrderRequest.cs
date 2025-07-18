namespace Lukki.Contracts.Orders;

public record CreateOrderRequest
(
    string Status,
    Address ShippingAddress,
    Address BillingAddress,
    List<InOrderProduct> InOrderProducts,
    string TargetCurrency
);

public record Address(
    string Street,
    string City,
    string PostalCode,
    string Country
);

public record InOrderProduct(
    uint Quantity,
    string Size,
    string ProductId
);