using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.HeaderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class HeaderRepository : IHeaderRepository
{
    private readonly LukkiDbContext _dbContext;

    public HeaderRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(MyHeader header)
    {
        _dbContext.Add(header);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MyHeader?> GetByNameAsync(string name)
    {
        return await _dbContext.Headers
            .FirstOrDefaultAsync(f => f.Name == name);
    }
    
    public async Task<List<string>> GetAllNamesAsync()
    {
        return await _dbContext.Headers
            .Select(b => b.Name)
            .ToListAsync();
    }
}