using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.MaterialAggregate;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Materials.Commands.CreateMaterial;

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, ErrorOr<Material>>
{
    
    private readonly IMaterialRepository _materialRepository;

    public CreateMaterialCommandHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<ErrorOr<Material>> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        
        
        // Validate the material doesn't exist
        if (await _materialRepository.GetByName(command.Name) is not null)
        {
            return Errors.Material.DuplicateName(command.Name);
        }
        
        
        
        // Create Material
        var material = Material.Create(
            name: command.Name
        );
        // Persist Material
        await _materialRepository.AddAsync(material);
        // Return Material
        return material;

    }
}