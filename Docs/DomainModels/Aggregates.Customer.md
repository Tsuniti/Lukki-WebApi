### Domain Models

## User

```csharp
class Customer{
    Customer();
    void AddProductToWishlist(Guid productId);
    void RemoveProductFromWishlist(Guid productId);
    void AddProductToCart(Guid inStockProductId, int quantity);
    void UpdateProductInCart(Guid inCartProductId, int newQuantity);
    void RemoveProductFromCart(Guid inStockProductId);
    void UpdateProfile(string firstName, string lastName, string email);

}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "John",
    "lastName": "Doe",
    "email": "JohnDoe@email.com",
    "passwordHash": "hashed_password",
    "role": "Customer",
    "phoneNumber": "+1234567890",
  "cartItems": [
    {
      "productId": "00000000-0000-0000-0000-000000000000",
      "size": "L",
      "quantity": 2
    }
  ],
  "inWishListProductIds": [
    "00000000-0000-0000-0000-000000000000"
  ],
  "orderIds": [
        "00000000-0000-0000-0000-000000000000"
    ],
  "reviewIds": [
    "00000000-0000-0000-0000-000000000000"
  ],
    "createdAt": "2023-10-01T00:00:00Z",
    "updatedAt": null
}
```
