using Lukki.Application.Categories.Queries.GetAllCategories;
using Lukki.Application.ProductBanners.Commands.CreateProductBanner;
using Lukki.Application.ProductBanners.Queries.GetProductBannerById;
using Lukki.Application.Products.Queries.GetProductsByIds;
using Lukki.Contracts.Banners;
using Lukki.Contracts.ProductBanners;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductAggregate;
using Lukki.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Error = ErrorOr.Error;

namespace Lukki.Api.Controllers;


[Route("product-banners")]
public class ProductBannersController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ProductBannersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    
    
    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [ProducesResponseType(typeof(ProductBannerResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateProductBanner(CreateProductBannerRequest request)
    {
        
        var command = _mapper.Map<CreateProductBannerCommand>(request);
        

        var createProductBannerResult = await _mediator.Send(command);
        
        return createProductBannerResult.Match(
            productBanner => Ok(_mapper.Map<ProductBannerResponse>(productBanner)),
            errors => Problem(errors) 
        );
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProductBannerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductBannerById([FromQuery]GetProductBannerRequest request)
    {
        var query = _mapper.Map<GetProductBannerByIdQuery>(request);

        var getProductBannerByIdResult = await _mediator.Send(query);

        var getAllCategoriesResult = await _mediator.Send(new GetAllCategoriesQuery());

        ProductBanner productBanner = null!;
        List<Error>? listErrors = null;

        getProductBannerByIdResult.Switch(
            productBannerResult => productBanner = productBannerResult,
            errors => listErrors = errors);
        if (listErrors is not null)
            return Problem(listErrors);
        
        var getProductsQuery = new GetProductsByIdsQuery(productBanner.ProductIds.Select(id => id.Value.ToString()).ToList());
        var getProductsResult = await _mediator.Send(getProductsQuery);
        var products = new List<Product>();
        getProductsResult.Switch(
            productsResult => products = productsResult,
            errors => listErrors = errors);
        if (listErrors is not null)
            return Problem(listErrors);
        
        var categories = getAllCategoriesResult.Match(
            categoriesResult => categoriesResult,
            errors => new List<Category>());
        
        
        return getProductBannerByIdResult.Match(
            productBannerResult => Ok(_mapper.Map<(ProductBanner, List<Product> Products, List<Category>), ProductBannerResponse>((productBannerResult, products, categories))),
            errors => Problem(errors));
    }
}