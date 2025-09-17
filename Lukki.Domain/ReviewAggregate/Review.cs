using Lukki.Domain.Common.Models;
using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.ReviewAggregate;

public sealed class Review  : AggregateRoot<ReviewId>
{
    public short Rating { get; private set; }
    public string Comment { get; private set; }
    public ProductId ProductId { get; private set; }
    public CustomerId? CustomerId { get; private set; }      // Nullable to save reviews if customer is deleted
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Review(
        ReviewId reviewId,
        short rating,
        string comment,
        ProductId productId,
        CustomerId customerId,
        DateTime createdAt
    ) : base(reviewId)
    {
        Rating = rating;
        Comment = comment;
        ProductId = productId;
        CustomerId = customerId;
        CreatedAt = createdAt;
    }
    
    public static Review Create(
        short rating,
        string comment,
        ProductId productId,
        CustomerId customerId
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
    
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Review()
    {
        // EF Core requires a parameterless constructor for materialization
    }
    #pragma warning restore CS8618
}