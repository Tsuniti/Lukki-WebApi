using FluentValidation;

namespace Lukki.Application.Footers.Queries.GetFooterByName;

public class GetFooterByNameQueryValidator : AbstractValidator<GetFooterByNameQuery>
{
    public GetFooterByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
    
}