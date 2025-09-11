using FluentValidation;

namespace Lukki.Application.Headers.Queries.GetHeaderByName;

public class GetHeaderByNameQueryValidator : AbstractValidator<GetHeaderByNameQuery>
{
    public GetHeaderByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
    
}