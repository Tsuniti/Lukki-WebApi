using Lukki.Domain.PromoCategoryAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IPromoCategoryRepository
{
    Task AddAsync(PromoCategory promoCategory);
    Task<PromoCategory?> GetByName(string name);
    Task<List<PromoCategory>> GetAllAsync();
}