using ErrorOr;
using Lukki.Domain.CategoryAggregate;
using MediatR;

namespace Lukki.Application.Categories.Queries.GetRootParentCategory;

public record GetRootParentCategoryQuery(
    string Id
    )
    : IRequest<ErrorOr<Category>>;
