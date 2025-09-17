using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.PromoCategories.Commands.CreatePromoCategory;

public class CreatePromoCategoryCommandValidator : AbstractValidator<CreatePromoCategoryCommand>
{

    public CreatePromoCategoryCommandValidator(IPromoCategoryRepository promoCategoryRepository)
    {
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
    }
}