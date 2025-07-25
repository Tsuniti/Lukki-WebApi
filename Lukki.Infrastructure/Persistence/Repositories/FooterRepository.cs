using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.FooterAggregate;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class FooterRepository : IFooterRepository
{
    private readonly LukkiDbContext _dbContext;

    public FooterRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Footer footer)
    {
        _dbContext.Add(footer);
        await _dbContext.SaveChangesAsync();
    }
}