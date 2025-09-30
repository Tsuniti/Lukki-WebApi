namespace Lukki.Contracts.Orders;

public record CreateOrderRequest
(
    AddressRequest ShippingAddress,
    AddressRequest BillingAddress,
    List<InOrderProductRequest> InOrderProducts,
    string TargetCurrency
);

public record AddressRequest(
    string Street,
    string City,
    string PostalCode,
    string Country
);

public record InOrderProductRequest(
    uint Quantity,
    string Size,
    string ProductId
);