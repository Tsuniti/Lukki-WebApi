using FluentValidation;

namespace Lukki.Application.ProductBanners.Queries.GetProductBannerById;

public class GetProductBannerByNameQueryValidator : AbstractValidator<GetProductBannerByIdQuery>
{
    public GetProductBannerByNameQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

    }
    
}