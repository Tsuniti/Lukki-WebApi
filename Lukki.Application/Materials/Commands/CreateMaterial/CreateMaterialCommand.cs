using ErrorOr;
using Lukki.Domain.MaterialAggregate;
using MediatR;

namespace Lukki.Application.Materials.Commands.CreateMaterial;

public record CreateMaterialCommand( 
    string Name
    ) : IRequest<ErrorOr<Material>>;