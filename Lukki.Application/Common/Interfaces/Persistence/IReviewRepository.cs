using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Application.Common.Interfaces.Persistence;

public interface IReviewRepository
{
    Task AddAsync(Review review);
    Task<bool> IsExistsReviewByCustomerIdAndProductIdAsync(CustomerId customerId, ProductId productId);
    Task<Review?> GetByIdAsync(ReviewId id);

    
    Task<List<Review>> GetListByReviewIdsAsync(IEnumerable<ReviewId> reviewIds);

}