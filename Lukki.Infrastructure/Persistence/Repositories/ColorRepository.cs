using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ColorAggregate;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ColorRepository : IColorRepository
{
    private readonly LukkiDbContext _dbContext;

    public ColorRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Color color)
    {
        _dbContext.Add(color);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Color?> GetByName(string name)
    {
        return _dbContext.Colors.FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<Color?> GetByIdAsync(ColorId id)
    {
        return await _dbContext.Colors.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Color>> GetAllAsync()
    {
        return await _dbContext.Colors.ToListAsync();
    }
}