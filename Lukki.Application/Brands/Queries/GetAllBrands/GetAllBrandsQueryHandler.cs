using Lukki.Domain.BrandAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryHandler :
    IRequestHandler<GetAllBrandsQuery, ErrorOr<List<Brand>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    
    public async Task<ErrorOr<List<Brand>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetAllAsync();

        if (brands is null || brands.Count == 0)
        {
            return Errors.Brand.NoBrandsFound();
        }

        return brands;
    }
}