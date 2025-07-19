using ErrorOr;
using Lukki.Domain.OrderAggregate;
using MediatR;

namespace Lukki.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string CustomerId,
    string Status,
    AddressCommand ShippingAddress,
    AddressCommand BillingAddress,
    List<InOrderProductCommand> InOrderProducts,
    string TargetCurrency
) : IRequest<ErrorOr<Order>>;

public record AddressCommand(
    string Street,
    string City,
    string PostalCode,
    string Country
);

public record InOrderProductCommand(
    uint Quantity,
    string Size,
    string ProductId
);