﻿namespace Lukki.Contracts.Orders;

public record OrderResponse
(
    string Id,
    string Status,
    MoneyResponse TotalAmount,
    AddressResponse ShippingAddress,
    AddressResponse BillingAddress,
    string CustomerId,
    List<InOrderProductResponse> InOrderProducts,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);


public record MoneyResponse(
    decimal Amount,
    string Currency);

public record AddressResponse(
    string Street,
    string City,
    string PostalCode,
    string Country
);

public record InOrderProductResponse(
    uint Quantity,
    string Size,
    string ProductId
);