namespace Lukki.Contracts.Products;

public record GetOneProductByIdRequest(
    string Id,
    string Currency = "USD"
    );