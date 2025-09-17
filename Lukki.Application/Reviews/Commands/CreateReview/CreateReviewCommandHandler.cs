using Lukki.Domain.ReviewAggregate;
using MediatR;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Reviews.Common;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ErrorOr<ReviewResult>>
{
    
    private readonly IReviewRepository _reviewRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public CreateReviewCommandHandler(IReviewRepository reviewRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
    {
        _reviewRepository = reviewRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }
    

    public async Task<ErrorOr<ReviewResult>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(CustomerId.Create(request.CustomerId));
        if (customer is null)
        {
            return Errors.Customer.NotFound(request.CustomerId);
        }
        var product = await _productRepository.GetByIdAsync(ProductId.Create(request.ProductId));
        if (product is null)
        {
            return Errors.Product.NotFound(request.ProductId);
        }
        if(await _reviewRepository.IsExistsReviewByCustomerIdAndProductIdAsync(customer.Id, product.Id))
        {
            //return Errors.Review.Duplicate(customerId: request.CustomerId, productId: request.ProductId);
        }
        
        var review = Review.Create(
            rating: request.Rating,
            comment: request.Comment,
            productId: product.Id,
            customerId: customer.Id
        );
        
        await _reviewRepository.AddAsync(review);
        product.AverageRating.AddNewRating(review.Rating);
        await _productRepository.Update(product);
        
        return new ReviewResult(
            review,
            customer.FirstName + " " + customer.LastName 
        );
        
    }
}