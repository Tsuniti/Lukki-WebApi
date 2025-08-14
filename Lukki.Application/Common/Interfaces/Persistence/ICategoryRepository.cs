using Lukki.Domain.CategoryAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<List<Category>> GetAllAsync();
}