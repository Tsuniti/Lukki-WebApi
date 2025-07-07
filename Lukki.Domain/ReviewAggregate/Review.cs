using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.ReviewAggregate;

public sealed class Category  : AggregateRoot<ReviewId>
{
    public int Rating { get; }
    public string Comment { get; }
    public ProductId ProductId { get; }
    public UserId CustomerId { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Category(
        ReviewId reviewId,
        int rating,
        string comment,
        ProductId productId,
        UserId customerId,
        DateTime createdAt
    ) : base(reviewId)
    {
        Rating = rating;
        Comment = comment;
        ProductId = productId;
        CustomerId = customerId;
        CreatedAt = createdAt;
    }
    
    public static Category Create(
        int rating,
        string comment,
        ProductId productId,
        UserId customerId
    )
    {
        return new(
            ReviewId.CreateUnique(),
            rating,
            comment,
            productId,
            customerId,
            DateTime.UtcNow
        );
    }

}