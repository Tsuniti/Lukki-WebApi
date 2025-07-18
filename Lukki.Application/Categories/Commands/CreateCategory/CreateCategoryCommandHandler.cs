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

    public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Validate Category Name is unique in the tree
        
        // Create Category
        var category = Category.Create(
            name: request.Name,
            parentCategoryId: request.ParentCategoryId is null
                ? null
                : CategoryId.Create(request.ParentCategoryId)
        );
        // Persist Category
        await _categoryRepository.AddAsync(category);
        // Return Category
        return category;

    }
}