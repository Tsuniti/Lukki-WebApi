using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CustomerAggregate;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly LukkiDbContext _dbContext;

    public CustomerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}