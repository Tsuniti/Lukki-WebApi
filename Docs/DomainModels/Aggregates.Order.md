### Domain Models

## Order

```csharp
class Order{
    Order();
    void UpdateStatus(OrderStatus newStatus);
    
}
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "status": "enum",
  "totalAmount": {
    "amount": 100.0,
    "currency": "USD"
  },
  "inOrderProducts": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "priceAtTimeOfOrder": {
        "amount": 50.0,
        "currency": "USD"
      },
      "size": "L",
      "quantity": 2,
      "productId": "00000000-0000-0000-0000-000000000000"
    }
  ],
  "shippingAddress": {
    "street": "123 Main St",
    "city": "Anytown",
    "zipCode": "12345",
    "country": "USA"
  },
  "billingAddress": {
    "street": "123 Main St",
    "city": "Anytown",
    "zipCode": "12345",
    "country": "USA"
  },
  "customerId": "00000000-0000-0000-0000-000000000000",
  "createdAt": "2023-10-01T00:00:00Z",
  "updatedAt": null
}
```
