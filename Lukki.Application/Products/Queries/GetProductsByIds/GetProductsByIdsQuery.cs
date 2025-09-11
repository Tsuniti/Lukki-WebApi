using Lukki.Domain.ProductAggregate;
using MediatR;
using ErrorOr;

namespace Lukki.Application.Products.Queries.GetProductsByIds;

public record GetProductsByIdsQuery(
    List<string> ProductIds
    ) : IRequest<ErrorOr<List<Product>>>;