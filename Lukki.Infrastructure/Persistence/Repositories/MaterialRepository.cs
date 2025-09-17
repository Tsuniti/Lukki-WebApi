using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.MaterialAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly LukkiDbContext _dbContext;

    public MaterialRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Material material)
    {
        _dbContext.Add(material);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Material?> GetByName(string name)
    {
        return _dbContext.Materials.FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<List<Material>> GetAllAsync()
    {
        return await _dbContext.Materials.ToListAsync();
    }
}