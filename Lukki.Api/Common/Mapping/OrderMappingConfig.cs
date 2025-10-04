using Lukki.Application.Orders.Commands.CreateOrder;
using Lukki.Contracts.Orders;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.Enums;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>(); // redundant, but for clarity that this is in use
        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.CustomerId, src => src.CustomerId != null ? src.CustomerId.Value.ToString() : null);

        TypeAdapterConfig<OrderId, string>.NewConfig().MapWith(id => id.Value.ToString());
        
        TypeAdapterConfig<CustomerId?, string?>.NewConfig()
            .MapWith(id => id != null ? id.Value.ToString() : null); // not working
        
        TypeAdapterConfig<OrderStatus, string>.NewConfig().MapWith(status => status.ToString());

    }
}