using Lukki.Domain.MaterialAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Materials.Queries.GetAllMaterials;

public class GetAllMaterialsQueryHandler :
    IRequestHandler<GetAllMaterialsQuery, ErrorOr<List<Material>>>
{
    private readonly IMaterialRepository _materialRepository;

    public GetAllMaterialsQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }
    
    public async Task<ErrorOr<List<Material>>> Handle(GetAllMaterialsQuery request, CancellationToken cancellationToken)
    {
        var materials = await _materialRepository.GetAllAsync();

        if (materials is null || materials.Count == 0)
        {
            return Errors.Material.NoMaterialsFound();
        }

        return materials;
    }
}