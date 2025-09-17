using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Products.Common;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using MediatR;

namespace Lukki.Application.Products.Queries.GetPagedProducts;

public class GetPagedProductsQueryHandler : 
    IRequestHandler<GetPagedProductsQuery, ErrorOr<PagedProductsResult>>
{
    
    private readonly IProductRepository _productRepository;
    private readonly IExchangeRateService _exchangeRateService; 


    public GetPagedProductsQueryHandler(IProductRepository productRepository, IExchangeRateService exchangeRateService)
    {
        _productRepository = productRepository;
        _exchangeRateService = exchangeRateService;
    }

    public async Task<ErrorOr<PagedProductsResult>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {

        await _exchangeRateService.GetRatesAsync();  // Ensure rates are up-to-date
        
        
        var filter = new ProductFilter(
           SearchTerm: request.SearchTerm,
           MinPrice: request.MinPrice,
           MaxPrice: request.MaxPrice,
           Currency: request.Currency,
           CategoryIds: request.CategoryIds?.ConvertAll(CategoryId.Create),
           PromoCategoryIds: request.PromoCategoryIds?.ConvertAll(PromoCategoryId.Create),
           BrandIds: request.BrandIds?.ConvertAll(BrandId.Create),
           ColorIds: request.ColorIds?.ConvertAll(ColorId.Create),
           MaterialIds: request.MaterialIds?.ConvertAll(MaterialId.Create),
           PageNumber: request.PageNumber,
           ItemsPerPage: request.ItemsPerPage,
           SortBy: request.SortBy
            );
        
        
        try
        {
            var result = await _productRepository.GetPagedAsync(filter);
            
            return new PagedProductsResult(
                Products: result.Products.Select(p => new PagedProductItemResult(
                    Id: p.Id.Value.ToString(),
                    Name: p.Name,
                    Price: new PagedProductItemMoneyResult(
                        Amount: Math.Round(p.Price.Amount, 2),
                        Currency: p.Price.Currency
                    ))).ToList(),
                CurrentPage: request.PageNumber,
                TotalPages: (int)Math.Ceiling((double)result.TotalItems / filter.ItemsPerPage),
                TotalItems: result.TotalItems
            );
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Exchange rate failed"))
        {
            return Errors.Product.ExchangeRateFailed(filter.Currency);
        }

    }
}