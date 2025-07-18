using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CategoryAggregate;

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
}