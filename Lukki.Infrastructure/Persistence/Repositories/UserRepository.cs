using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LukkiDbContext _dbContext;

    public UserRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(IUser user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IUser?> GetUserByEmailAsync(string email)
    {
        
        var seller = await _dbContext.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Email == email);

        return seller;
    }
}