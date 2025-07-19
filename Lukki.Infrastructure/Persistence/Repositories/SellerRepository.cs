using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly LukkiDbContext _dbContext;

    public SellerRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}