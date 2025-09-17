using Lukki.Application.Materials.Commands.CreateMaterial;
using Lukki.Application.Materials.Queries.GetAllMaterials;
using Lukki.Contracts.Materials;
using Lukki.Domain.MaterialAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("materials")]
public class MaterialsController : ApiController
{
    
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public MaterialsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(Material), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateMaterial(CreateMaterialRequest request)
    {
        var command = _mapper.Map<CreateMaterialCommand>(request);
        
        var createMaterialResult = await _mediator.Send(command);

        
        return createMaterialResult.Match(
            material => Ok(_mapper.Map<MaterialResponse>(material)),
            errors => Problem(errors) 
        );
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Material), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMaterials()
    {
        var getAllMaterialsResult = await _mediator.Send(new GetAllMaterialsQuery());

        return getAllMaterialsResult.Match(
            materialsResult => Ok(_mapper.Map<List<MaterialResponse>>(materialsResult)),
            errors => Problem(errors));
    }
}