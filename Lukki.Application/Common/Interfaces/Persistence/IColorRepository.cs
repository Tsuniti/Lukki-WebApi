using Lukki.Domain.ColorAggregate;
using Lukki.Domain.ColorAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IColorRepository
{
    Task AddAsync(Color color);
    Task<Color?> GetByName(string name);
    Task<Color?> GetByIdAsync(ColorId id);

    Task<List<Color>> GetAllAsync();
}