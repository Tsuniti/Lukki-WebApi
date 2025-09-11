using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LukkiDbContext _dbContext;

    public CategoryRepository(LukkiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Category category)
    {
        _dbContext.Add(category);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Category>> GetAllAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetRootParentAsync(CategoryId id)
    {
        var current = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        while (current.ParentId != null)
        {
            current = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == current.ParentId);
            if (current == null) break;
        }
        return current;
    }
}