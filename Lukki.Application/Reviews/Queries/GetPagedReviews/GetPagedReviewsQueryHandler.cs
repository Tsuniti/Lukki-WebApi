using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Reviews.Common;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Reviews.Queries.GetPagedReviews;

public class GetPagedReviewsQueryHandler : 
    IRequestHandler<GetPagedReviewsQuery, ErrorOr<PagedReviewsResult>>
{
    
    private readonly IReviewRepository _reviewRepository;
    private readonly ICustomerRepository _customerRepository;

    public GetPagedReviewsQueryHandler(IReviewRepository reviewRepository, ICustomerRepository customerRepository)
    {
        _reviewRepository = reviewRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<PagedReviewsResult>> Handle(GetPagedReviewsQuery request, CancellationToken cancellationToken)
    {


            var (reviews, totalItems) = await _reviewRepository.GetPagedAsync(
                productId: request.ProductId,
                pageNumber: request.PageNumber,
                itemsPerPage: request.ItemsPerPage,
                sortBy: request.SortBy);
            
            var reviewItems = new List<ReviewResult>();
            
            foreach (var r in reviews)
            {
                Customer? customer = null;
                if (r.CustomerId is not null)
                {
                    customer = await _customerRepository.GetByIdAsync(r.CustomerId);
                }

                reviewItems.Add(new ReviewResult(
                    Review: r,
                    CustomerName: (customer?.FirstName ?? "Unknown") + " " + (customer?.LastName ?? "")
                ));
            }

            return new PagedReviewsResult(
                Reviews: reviewItems,
                CurrentPage: request.PageNumber,
                TotalPages: (int)Math.Ceiling((double)totalItems / request.ItemsPerPage),
                TotalItems: totalItems
            );
        

    }
}