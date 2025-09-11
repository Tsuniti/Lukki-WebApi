using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductBannerAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IProductBannerRepository
{
    Task AddAsync(ProductBanner banner);
    Task<ProductBanner?> GetByIdAsync(ProductBannerId id);
}