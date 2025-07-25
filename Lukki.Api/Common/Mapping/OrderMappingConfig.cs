using Lukki.Application.Orders.Commands.CreateOrder;
using Lukki.Contracts.Orders;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Mapster;

namespace Lukki.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>(); // redundant, but for clarity that this is in use

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        TypeAdapterConfig<OrderId, Guid>.NewConfig().MapWith(id => id.Value);

    }
}