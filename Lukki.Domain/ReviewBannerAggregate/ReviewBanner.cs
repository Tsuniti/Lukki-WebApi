using Lukki.Domain.ReviewBannerAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.ReviewAggregate.ValueObjects;

namespace Lukki.Domain.ReviewBannerAggregate;

public sealed class ReviewBanner : AggregateRoot<ReviewBannerId>
{
    private readonly List<ReviewId> _reviewIds = new();
    
    public string Title { get; private set; }
    public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private ReviewBanner(
        ReviewBannerId reviewBannerId,
        string title,
        List<ReviewId> reviewIds,
        DateTime createdAt
    ) : base(reviewBannerId)
    {
        Title = title;
        _reviewIds = reviewIds ?? new List<ReviewId>();
        CreatedAt = createdAt;
    }
    
    public static ReviewBanner Create(
        string title,
        List<ReviewId> reviewIds
    )
    {
        return new(
            ReviewBannerId.CreateUnique(),
            title,
            reviewIds,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private ReviewBanner()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}