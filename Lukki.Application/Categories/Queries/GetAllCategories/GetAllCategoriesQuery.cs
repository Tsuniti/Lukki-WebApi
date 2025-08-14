using Lukki.Domain.CategoryAggregate;
using ErrorOr;
using MediatR;

namespace Lukki.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<ErrorOr<List<Category>>>;