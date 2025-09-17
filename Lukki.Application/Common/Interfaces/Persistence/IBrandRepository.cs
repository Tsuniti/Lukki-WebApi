using Lukki.Domain.BrandAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IBrandRepository
{
    Task AddAsync(Brand brand);
    Task<Brand?> GetByName(string name);
    Task<List<Brand>> GetAllAsync();
}