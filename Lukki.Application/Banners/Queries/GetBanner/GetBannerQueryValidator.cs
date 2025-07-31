using FluentValidation;
using Lukki.Application.Banners.Queries.GetBanner;

namespace Lukki.Application.Authentication.Queries.Login;

public class GetBannerQueryValidator : AbstractValidator<GetBannerQuery>
{
    public GetBannerQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
    
}