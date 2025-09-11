using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetRootParentAsync(CategoryId id);
}