using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Colors.Commands.CreateColor;

public class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
{

    public CreateColorCommandValidator(IColorRepository colorRepository)
    {
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
    }
}