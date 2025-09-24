using ErrorOr;
using Lukki.Application.Products.Common;
using Lukki.Domain.ProductAggregate;
using MediatR;

namespace Lukki.Application.Products.Queries.GetOneProductById;

public record GetOneProductByIdQuery(
    string Id,
    string Currency
    ) : IRequest<ErrorOr<ProductResult>>;