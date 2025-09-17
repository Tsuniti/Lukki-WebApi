using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Lukki.Domain.ReviewBannerAggregate;
using MediatR;

namespace Lukki.Application.ReviewBanners.Commands.CreateReviewBanner;

public class CreateReviewBannerCommandHandler : IRequestHandler<CreateReviewBannerCommand, ErrorOr<ReviewBanner>>
{

    private readonly IReviewBannerRepository _reviewBannerRepository;


    public CreateReviewBannerCommandHandler(IReviewBannerRepository reviewBannerRepository)
    {
        _reviewBannerRepository = reviewBannerRepository;
    }

    public async Task<ErrorOr<ReviewBanner>> Handle(CreateReviewBannerCommand command, CancellationToken cancellationToken)
    {
        
        // Create ReviewBanner
        var reviewBanner = ReviewBanner.Create(
            title: command.Title,
            reviewIds: command.ReviewIds
                .Select(ReviewId.Create)
                .ToList() 
        );
        
        // Persist ReviewBanner
        await _reviewBannerRepository.AddAsync(reviewBanner);
        // Return ReviewBanner
        return reviewBanner;
        
        
    }
}