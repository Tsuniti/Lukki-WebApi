using Lukki.Domain.BannerAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IBannerRepository
{
    Task AddAsync(Banner banner);
    Task<Banner?> GetByNameAsync(string name);
}