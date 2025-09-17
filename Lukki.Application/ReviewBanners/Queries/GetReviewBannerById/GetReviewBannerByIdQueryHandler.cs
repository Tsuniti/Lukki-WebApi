using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.ReviewBanners.Common;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.ReviewBannerAggregate;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.ReviewBanners.Queries.GetReviewBannerById;

public class GetReviewBannerByIdQueryHandler : 
    IRequestHandler<GetReviewBannerByIdQuery, ErrorOr<ReviewBannerResult>>
{
    
    private readonly IReviewBannerRepository _reviewBannerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;


    public GetReviewBannerByIdQueryHandler(IReviewBannerRepository reviewBannerRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IReviewRepository reviewRepository)
    {
        _reviewBannerRepository = reviewBannerRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<ErrorOr<ReviewBannerResult>> Handle(GetReviewBannerByIdQuery query, CancellationToken cancellationToken)
    {
        
        if (await _reviewBannerRepository.GetByIdAsync(ReviewBannerId.Create(query.Id)) is not ReviewBanner reviewBanner)
        {
            return Errors.ReviewBanner.NotFound(query.Id);
        }
        
        var reviewBannerItems = new List<ReviewBannerItem>();
        
        foreach (var reviewId in reviewBanner.ReviewIds)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review is null)
            {
                continue; // Skip if review not found
            }

            var customer = await _customerRepository.GetByIdAsync(review.CustomerId);
            var product = await _productRepository.GetByIdAsync(review.ProductId);
            
            var customerName = customer is not null ? customer.FirstName + " " + customer.LastName : "Unknown Customer";
            var productName = product?.Name ?? "Unknown Product";
            var productImageUrl = product?.Images.FirstOrDefault()?.Url ?? string.Empty;

            var reviewBannerItem = new ReviewBannerItem(
                Review: review,
                CustomerName: customerName,
                ProductName: productName,
                ProductImageUrl: productImageUrl,
                CreatedAt: review.CreatedAt,
                UpdatedAt: review.UpdatedAt
            );
            
            reviewBannerItems.Add(reviewBannerItem);
        }
        
        var reviewBannerResult = new ReviewBannerResult(reviewBanner.Id, reviewBanner.Title, reviewBannerItems);
        
        
        return reviewBannerResult;
    }
}