using Lukki.Domain.MaterialAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IMaterialRepository
{
    Task AddAsync(Material material);
    Task<Material?> GetByName(string name);
    Task<List<Material>> GetAllAsync();
}