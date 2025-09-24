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
   // private readonly ICurrencyConverter _currencyConverter;


    public GetPagedProductsQueryHandler(IProductRepository productRepository, IExchangeRateService exchangeRateService)
    {
        _productRepository = productRepository;
        _exchangeRateService = exchangeRateService;
   //     _currencyConverter = currencyConverter;
    }

    public async Task<ErrorOr<PagedProductsResult>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {

        var exchangeRates = await _exchangeRateService.GetRatesAsync();  // Ensure rates are up-to-date
        
        
        var filter = new ProductFilter(
           SearchTerm: request.SearchTerm,
           MinPrice: request.MinPrice,
           MaxPrice: request.MaxPrice,
           Currency: request.Currency,
           CategoryIds: request.CategoryIds?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(CategoryId.Create)
            .ToList(),
           PromoCategoryIds: request.PromoCategoryIds?
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(PromoCategoryId.Create)
               .ToList(),
           BrandIds: request.BrandIds?
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(BrandId.Create)
               .ToList(),
           ColorIds: request.ColorIds?
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(ColorId.Create)
               .ToList(),
           MaterialIds: request.MaterialIds?
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(MaterialId.Create)
               .ToList(),
           PageNumber: request.PageNumber,
           ItemsPerPage: request.ItemsPerPage,
           SortBy: request.SortBy
            );
        
        
        try
        {
            var result = await _productRepository.GetPagedAsync(filter);

            var productItems = new List<PagedProductItemResult>();

            foreach (var p in result.Products)
            {
                // var convertedPrice = await _currencyConverter.ConvertAsync(p.Price, filter.Currency);

                p.Price.Convert(filter.Currency, exchangeRates);
                
                productItems.Add(new PagedProductItemResult(
                    Id: p.Id.Value.ToString(),
                    Name: p.Name,
                    Price: new PagedProductItemMoneyResult(
                        Amount: p.Price.Amount,
                        Currency: p.Price.Currency
                    )
                ));
            }

            return new PagedProductsResult(
                Products: productItems.ToList(),
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