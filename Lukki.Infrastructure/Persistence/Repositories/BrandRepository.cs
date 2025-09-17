using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.BrandAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly LukkiDbContext _dbContext;

    public BrandRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Brand brand)
    {
        _dbContext.Add(brand);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Brand?> GetByName(string name)
    {
        return _dbContext.Brands.FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _dbContext.Brands.ToListAsync();
    }
}