using Lukki.Domain.BrandAggregate;
using ErrorOr;
using MediatR;

namespace Lukki.Application.Brands.Queries.GetAllBrands;

public class GetAllBrandsQuery : IRequest<ErrorOr<List<Brand>>>;