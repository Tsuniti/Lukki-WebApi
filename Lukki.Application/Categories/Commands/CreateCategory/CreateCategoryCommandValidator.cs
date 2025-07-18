using FluentValidation;
using Lukki.Application.Common.Interfaces.Persistence;

namespace Lukki.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        
        RuleFor(x => x.Name)
            .NotEmpty();
        
    }
}