using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
    
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        
        // Create Category
        var category = Category.Create(
            name: command.Name,
            parentCategoryId: command.ParentCategoryId is null
                ? null
                : CategoryId.Create(command.ParentCategoryId)
        );
        // Persist Category
        await _categoryRepository.AddAsync(category);
        // Return Category
        return category;

    }
}