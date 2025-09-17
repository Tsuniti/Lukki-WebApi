using Lukki.Domain.ReviewBannerAggregate;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IReviewBannerRepository
{
    Task AddAsync(ReviewBanner banner);
    Task<ReviewBanner?> GetByIdAsync(ReviewBannerId id);
}