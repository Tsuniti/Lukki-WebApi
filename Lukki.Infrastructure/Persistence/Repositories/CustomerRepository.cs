using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly LukkiDbContext _dbContext;

    public CustomerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Customer customer)
    {
        _dbContext.Add(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
        
            return customer;
    }
    public async Task<Customer?> GetByIdAsync(CustomerId id)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
            return customer;
    }
}