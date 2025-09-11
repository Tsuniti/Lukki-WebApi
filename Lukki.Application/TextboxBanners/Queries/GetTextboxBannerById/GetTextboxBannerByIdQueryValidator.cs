using FluentValidation;

namespace Lukki.Application.TextboxBanners.Queries.GetTextboxBannerById;

public class GetTextboxBannerByNameQueryValidator : AbstractValidator<GetTextboxBannerByIdQuery>
{
    public GetTextboxBannerByNameQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

    }
    
}