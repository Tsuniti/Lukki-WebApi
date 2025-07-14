using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Interfaces;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LukkiDbContext _dbContext;

    public UserRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(IUser user)
    {
        _dbContext.Add(user);
        _dbContext.SaveChanges();
    }

    public IUser? GetUserByEmail(string email)
    {
        var customer =  _dbContext.Customers
            .FirstOrDefault(c => c.Email == email);

        if (customer is not null)
            return customer;

        var seller =  _dbContext.Sellers
            .FirstOrDefault(s => s.Email == email);

        return seller;
    }
}