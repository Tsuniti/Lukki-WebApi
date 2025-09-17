using Lukki.Domain.ReviewAggregate;
using MediatR;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Reviews.Common;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Application.Reviews.Queries.GetReviewsByIds;

public class GetReviewsByIdsQueryHandler : 
    IRequestHandler<GetReviewsByIdsQuery, ErrorOr<List<ReviewResult>>>
{
    
    private readonly IReviewRepository _reviewRepository;
    private readonly ICustomerRepository _customerRepository;

    public GetReviewsByIdsQueryHandler(IReviewRepository reviewRepository, ICustomerRepository customerRepository)
    {
        _reviewRepository = reviewRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<List<ReviewResult>>> Handle(GetReviewsByIdsQuery request, CancellationToken cancellationToken)
    {
        var reviewIds = request.ReviewIds
            .Select(id => ReviewId.Create(id))
            .ToList();
        
        var reviews = await _reviewRepository.GetListByReviewIdsAsync(reviewIds);
    
        if (reviews.Count != request.ReviewIds.Count)
        {
            var missingIds = reviewIds.Except(reviews.Select(p => p.Id));
            return Errors.Review.NotFoundByIds(missingIds);
        }

        
        var reviewResults = new List<ReviewResult>();

        foreach (var review in reviews)
        {
            var customer = await _customerRepository.GetByIdAsync(review.CustomerId);
            if (customer != null)
            {
                reviewResults.Add(new ReviewResult(
                    review,
                    customer.FirstName + " " + customer.LastName
                ));
            }
        }

        return reviewResults;
        
    }
}