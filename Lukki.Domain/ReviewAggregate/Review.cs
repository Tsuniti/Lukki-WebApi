using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.ReviewAggregate;

public sealed class Review  : AggregateRoot<ReviewId, Guid>
{
    public uint Rating { get; }
    public string Comment { get; }
    public ProductId ProductId { get; }
    public UserId CustomerId { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Review(
        ReviewId reviewId,
        uint rating,
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
    
    public static Review Create(
        uint rating,
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
    
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Review()
    {
        // EF Core requires a parameterless constructor for materialization
    }
    #pragma warning restore CS8618
}