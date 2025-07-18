using ErrorOr;
using Lukki.Domain.CategoryAggregate;
using MediatR;

namespace Lukki.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand( 
    string Name,
    string? ParentCategoryId
    ) : IRequest<ErrorOr<Category>>;