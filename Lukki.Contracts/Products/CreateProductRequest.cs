﻿namespace Lukki.Contracts.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    string TargetGroup,
    Price Price,
    string CategoryId,
    //Image does not fall into contracts, because This is IFormFile
    List<InStockProductRequest> InStockProducts
);


public record Price(
    decimal Amount,
    string Currency);

public record InStockProductRequest(
    uint Quantity,
    string Size);
    