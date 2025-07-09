### Domain Models

## Product

```csharp
class Product{
    Product();
    void UpdateInStockProductQuantity(Size size, int newQuantity);
    void AddImage(Image image);
    void RemoveImage(string imageUrl);
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",

  "name": "string",
  "description": "string",
  "targetGroup": "MALE",
  "averageRating": 4.5,
  "price": {
    "amount": 100.0,
    "currency": "USD"
  },
  "images": [
    {
      "url": "https://example.com/image.jpg"
    }
  ],
  "inStockProducts": [
    {
      "quantity": 100,
      "size": "L"
    }
  ],
  "reviewIds": [
    "00000000-0000-0000-0000-000000000000"
  ],
  "categoryId": {
    "id":  "00000000-0000-0000-0000-000000000000"
},
  "createdAt": "2023-10-01T00:00:00Z",
  "updatedAt": null
}
```
