using Lukki.Domain.MaterialAggregate;
using ErrorOr;
using MediatR;

namespace Lukki.Application.Materials.Queries.GetAllMaterials;

public class GetAllMaterialsQuery : IRequest<ErrorOr<List<Material>>>;