using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly LukkiDbContext _dbContext;

    public CustomerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}