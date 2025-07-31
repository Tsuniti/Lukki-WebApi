using FluentValidation;

namespace Lukki.Application.Footers.Queries.GetFooter;

public class GetFooterQueryValidator : AbstractValidator<Footers.Queries.GetFooter.GetFooterQuery>
{
    public GetFooterQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
    
}