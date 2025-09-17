using Lukki.Application.Materials.Commands.CreateMaterial;
using Lukki.Contracts.Materials;
using Lukki.Domain.MaterialAggregate;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Mapster;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Api.Common.Mapping;

public class MaterialMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMaterialRequest, CreateMaterialCommand>();

        config.NewConfig<Material, MaterialResponse>();
        
        TypeAdapterConfig<MaterialId, string>.NewConfig().MapWith(id => id.Value.ToString());
    }
}