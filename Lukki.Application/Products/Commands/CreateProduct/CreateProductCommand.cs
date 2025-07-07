using Lukki.Domain.ProductAggregate;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string SellerId,
    string Name,
    string Description,
    string TargetGroup,
    PriceCommand Price,
    List<string> Images,
    List<InStockProductCommand> InStockProducts,
    List<string> CategoryIds
) : IRequest<ErrorOr<Product>>;


public record PriceCommand(
    decimal Amount,
    string Currency);

public record InStockProductCommand(
    uint Quantity,
    string Size);
