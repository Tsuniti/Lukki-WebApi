using ErrorOr;
using Lukki.Domain.BrandAggregate;
using MediatR;

namespace Lukki.Application.Brands.Commands.CreateBrand;

public record CreateBrandCommand( 
    string Name,
    Stream Image
    ) : IRequest<ErrorOr<Brand>>;