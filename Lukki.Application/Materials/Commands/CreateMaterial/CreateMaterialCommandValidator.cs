using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Materials.Commands.CreateMaterial;

public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{

    public CreateMaterialCommandValidator(IMaterialRepository materialRepository)
    {
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
    }
}