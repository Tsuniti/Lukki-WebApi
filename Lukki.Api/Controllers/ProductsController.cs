using System.Security.Claims;
using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Contracts.Products;
using Lukki.Domain.ProductAggregate;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("products")]
public class ProductsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ProductsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateProduct(
        [FromForm]CreateProductRequest request, 
        [FromForm]List<IFormFile> images)
    {
        
        
        var command = _mapper.Map<CreateProductCommand>(request);

        var streamImages = await FileHelpers.ConvertToStreamsAsync(images);
        
        var createProductResult = await _mediator.Send(command with 
            { Images = streamImages });
        
        return createProductResult.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            errors => Problem(errors) 
            );
    }
}