using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.Enums;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LukkiDbContext _dbContext;

    public OrderRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Order order)
    {
        _dbContext.Add(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(OrderId id)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task MarkAsPaidAsync(string paymentIntentId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntentId);
        if (order is not null)
        {
            order.MarkAsPaid();
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}