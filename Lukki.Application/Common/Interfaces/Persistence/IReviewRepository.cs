using Lukki.Domain.ReviewAggregate;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IReviewRepository
{
    Task AddAsync(Review review);
}