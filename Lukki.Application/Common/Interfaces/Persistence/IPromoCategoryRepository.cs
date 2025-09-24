using Lukki.Domain.PromoCategoryAggregate;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IPromoCategoryRepository
{
    Task AddAsync(PromoCategory promoCategory);
    Task<PromoCategory?> GetByName(string name);
    Task<List<PromoCategory>> GetListByIdsAsync(IReadOnlyList<PromoCategoryId> ids);
    Task<List<PromoCategory>> GetAllAsync();
}