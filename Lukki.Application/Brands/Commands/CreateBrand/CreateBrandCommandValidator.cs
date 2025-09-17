using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{

    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
    }
}