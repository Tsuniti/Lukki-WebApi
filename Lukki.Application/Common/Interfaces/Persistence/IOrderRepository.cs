using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(OrderId id);
    
    Task MarkAsPaidAsync(string paymentIntentId);
}