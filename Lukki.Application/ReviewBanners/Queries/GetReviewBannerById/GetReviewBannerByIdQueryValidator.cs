using FluentValidation;

namespace Lukki.Application.ReviewBanners.Queries.GetReviewBannerById;

public class GetReviewBannerByNameQueryValidator : AbstractValidator<GetReviewBannerByIdQuery>
{
    public GetReviewBannerByNameQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

    }
    
}