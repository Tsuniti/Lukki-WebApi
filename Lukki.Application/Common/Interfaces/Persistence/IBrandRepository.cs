using Lukki.Domain.BrandAggregate;
using Lukki.Domain.BrandAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IBrandRepository
{
    Task AddAsync(Brand brand);
    Task<Brand?> GetByNameAsync(string name);
    Task<Brand?> GetByIdAsync(BrandId id);
    Task<List<Brand>> GetAllAsync();
}