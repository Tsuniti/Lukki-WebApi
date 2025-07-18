using Lukki.Domain.OrderAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}