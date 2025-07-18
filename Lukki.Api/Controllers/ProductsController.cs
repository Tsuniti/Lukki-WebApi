using System.Security.Claims;
using Lukki.Application.Products.Commands.CreateProduct;
using Lukki.Contracts.Products;
using Lukki.Domain.Common.Enums;
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
    [Authorize(Roles = nameof(UserRole.SELLER))]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
        if (string.IsNullOrEmpty(sellerId))
        {
            return Unauthorized("Seller ID not found in token");
        }
        
        
        var command = _mapper.Map<CreateProductCommand>(request);

        var createProductResult = await _mediator.Send(command with { SellerId = sellerId });
        
        return createProductResult.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            errors => Problem(errors) 
            );
    }
}