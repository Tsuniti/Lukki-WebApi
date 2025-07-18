using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.Entities;
using Lukki.Domain.OrderAggregate.Enums;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrencyConverter _currencyConverter; 

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, ICurrencyConverter currencyConverter)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        
        var productIds = request.InOrderProducts
            .Select(iop => ProductId.Create(iop.ProductId))
            .ToList();

        var existingProducts = await _productRepository.GetProductsByProductIdsAsync(productIds);
    
        if (existingProducts.Count != productIds.Count)
        {
            var missingIds = productIds.Except(existingProducts.Select(p => p.Id));
            return Errors.Product.NotFoundByIds(missingIds);
        }
        
        
        var productsDict = existingProducts
            .ToDictionary(p => p.Id.Value.ToString()); // Используем Guid как ключ
        
        var inOrderProducts = new List<InOrderProduct>(request.InOrderProducts.Count);
        
        var totalAmount = Money.Create(0, request.TargetCurrency);

        foreach (var requestItem in request.InOrderProducts)
        {
            var product = productsDict[requestItem.ProductId];
            
            var subtotal = product.Price.Amount * requestItem.Quantity;
            
            var convertedMoney =  await _currencyConverter.ConvertAsync(
                    money: product.Price,
                    toCurrency: request.TargetCurrency
                );
            
            // Calculate total amount
            totalAmount = totalAmount.Add(
                Money.Create(
                    amount: convertedMoney.Amount * requestItem.Quantity,
                    currency: convertedMoney.Currency
                )
            );
            
            inOrderProducts.Add(InOrderProduct.Create(
                priceAtTimeOfOrder: product.Price,
                quantity: requestItem.Quantity,
                size: requestItem.Size,
                productId: product.Id
            ));
        }


        var order = Order.Create(
            status: Enum.Parse<OrderStatus>(request.Status, true),
            totalAmount: totalAmount,
            
            shippingAddress: Address.Create(
                street: request.ShippingAddress.Street,
                city: request.ShippingAddress.City,
                postalCode: request.ShippingAddress.PostalCode,
                country: request.ShippingAddress.Country),
        
            billingAddress: Address.Create(
                street: request.BillingAddress.Street,
                city: request.BillingAddress.City,
                postalCode: request.BillingAddress.PostalCode,
                country: request.BillingAddress.Country),
            
            customerId: UserId.Create(request.CustomerId),
            inOrderProducts: inOrderProducts
            
        );
        // Persist Order
        await _orderRepository.AddAsync(order);
        // Return Order
        return order;

    }
}