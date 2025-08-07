using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.FooterAggregate;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Footer?> GetByNameAsync(string name)
    {
        return await _dbContext.Footers
            .FirstOrDefaultAsync(f => f.Name == name);
    }
    
    public async Task<List<string>> GetAllNamesAsync()
    {
        return await _dbContext.Footers
            .Select(b => b.Name)
            .ToListAsync();
    }
}