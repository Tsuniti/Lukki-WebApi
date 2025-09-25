using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.Entities;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IExchangeRateService _exchangeRateService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IExchangeRateService exchangeRateService)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _exchangeRateService = exchangeRateService;
    }

    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var productIds = command.InOrderProducts
            .Select(iop => ProductId.Create(iop.ProductId))
            .ToList();

        var existingProducts = await _productRepository.GetListByIdsAsync(productIds);
    
        if (existingProducts.Count != productIds.Count)
        {
            var missingIds = productIds.Except(existingProducts.Select(p => p.Id));
            return Errors.Product.NotFoundByIds(missingIds);
        }
        
        
        var productsDict = existingProducts
            .ToDictionary(p => p.Id.Value.ToString()); // Используем Guid как ключ
        
        var inOrderProducts = new List<InOrderProduct>(command.InOrderProducts.Count);
        
        var totalAmount = Money.Create(0, command.TargetCurrency);

        foreach (var requestItem in command.InOrderProducts)
        {
            var product = productsDict[requestItem.ProductId];
            
            var subtotal = product.Price.Amount * requestItem.Quantity;
            
            product.Price.Convert(command.TargetCurrency, await _exchangeRateService.GetRatesAsync());
            
            // Calculate total amount
            totalAmount = totalAmount.Add(product.Price);
            
            inOrderProducts.Add(InOrderProduct.Create(
                priceAtTimeOfOrder: product.Price,
                quantity: requestItem.Quantity,
                size: requestItem.Size,
                productId: product.Id
            ));
        }


        var order = Order.Create(
            totalAmount: totalAmount,
            shippingAddress: Address.Create(
                street: command.ShippingAddress.Street,
                city: command.ShippingAddress.City,
                postalCode: command.ShippingAddress.PostalCode,
                country: command.ShippingAddress.Country),
        
            billingAddress: Address.Create(
                street: command.BillingAddress.Street,
                city: command.BillingAddress.City,
                postalCode: command.BillingAddress.PostalCode,
                country: command.BillingAddress.Country),
            
            customerId: command.CustomerId is not null ? CustomerId.Create(command.CustomerId) : null,
            inOrderProducts: inOrderProducts
            
        );
        // Persist Order
        await _orderRepository.AddAsync(order);
        // Return Order
        return order;

    }
}