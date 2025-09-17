using Lukki.Application.Brands.Commands.CreateBrand;
using Lukki.Application.Brands.Queries.GetAllBrands;
using Lukki.Contracts.Brands;
using Lukki.Domain.BrandAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("brands")]
public class BrandsController : ApiController
{
    
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public BrandsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateBrand([FromForm]CreateBrandRequest request, [FromForm]IFormFile image)
    {
        var command = _mapper.Map<CreateBrandCommand>(request);
        
        var streamImage = await FileHelpers.ConvertToStreamAsync(image);
        
        var createBrandResult = await _mediator.Send(command with { Image = streamImage });

        
        return createBrandResult.Match(
            brand => Ok(_mapper.Map<BrandResponse>(brand)),
            errors => Problem(errors) 
        );
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBrands()
    {
        var getAllBrandsResult = await _mediator.Send(new GetAllBrandsQuery());

        return getAllBrandsResult.Match(
            brandsResult => Ok(_mapper.Map<List<BrandResponse>>(brandsResult)),
            errors => Problem(errors));
    }
}