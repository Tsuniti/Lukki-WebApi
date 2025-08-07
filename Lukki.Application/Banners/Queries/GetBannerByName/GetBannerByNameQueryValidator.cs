using FluentValidation;

namespace Lukki.Application.Banners.Queries.GetBannerByName;

public class GetBannerByNameQueryValidator : AbstractValidator<GetBannerByNameQuery>
{
    public GetBannerByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
    
}