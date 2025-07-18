using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.OrderAggregate;

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
}