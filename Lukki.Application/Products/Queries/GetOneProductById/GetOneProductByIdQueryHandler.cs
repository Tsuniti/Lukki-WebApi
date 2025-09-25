using Lukki.Domain.ProductAggregate;
using MediatR;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Products.Common;
using Lukki.Domain.BrandAggregate;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.ColorAggregate;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.ProductAggregate.ValueObjects;

namespace Lukki.Application.Products.Queries.GetOneProductById;

public class GetOneProductByIdQueryHandler : 
    IRequestHandler<GetOneProductByIdQuery, ErrorOr<ProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPromoCategoryRepository _promoCategoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IColorRepository _colorRepository;

    public GetOneProductByIdQueryHandler(IProductRepository productRepository, IExchangeRateService exchangeRateService, ICategoryRepository categoryRepository, IPromoCategoryRepository promoCategoryRepository, IBrandRepository brandRepository, IColorRepository colorRepository)
    {
        _productRepository = productRepository;
        _exchangeRateService = exchangeRateService;
        _categoryRepository = categoryRepository;
        _promoCategoryRepository = promoCategoryRepository;
        _brandRepository = brandRepository;
        _colorRepository = colorRepository;
    }

    public async Task<ErrorOr<ProductResult>> Handle(GetOneProductByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _productRepository.GetByIdAsync(ProductId.Create(request.Id)) is not Product product)
        {
            return Errors.Product.NotFound(request.Id);
        }
        
        product.Price.Convert(request.Currency, await _exchangeRateService.GetRatesAsync());


        var allCategories = await _categoryRepository.GetAllAsync();
        
        if (allCategories.FirstOrDefault(c => c.Id == product.CategoryId) is not Category category)
            return Errors.Category.NotFoundById(product.CategoryId.Value.ToString());
        
        List<CategoryPath> categoryPath = new List<CategoryPath>();
        
        var cycleCategory = category;
        while (cycleCategory is not null)
        {

            Category? parent = null;
            if (cycleCategory.ParentId is not null)
            {
                parent = allCategories.FirstOrDefault(
                    c => c.Id?.Equals(cycleCategory.ParentId) is true);
            }
            
            
            categoryPath.Insert(0, new CategoryPath(cycleCategory.Id.Value.ToString(), cycleCategory.Name));
            cycleCategory = parent;

        }
        var promoCategoriesResult = await _promoCategoryRepository.GetListByIdsAsync(product.PromoCategoryIds);
        if (promoCategoriesResult.Count < product.PromoCategoryIds.Count)
            return Errors.PromoCategory.OneOrMoreNotFoundById(
                promoCategoriesResult.Select(pc => pc.Id.Value.ToString()).ToList(),
                product.PromoCategoryIds.Select(id => id.Value.ToString()).ToList());

        if (await _brandRepository.GetByIdAsync(product.BrandId) is not Brand brandResult)
            return Errors.Brand.NotFoundById(product.BrandId.Value.ToString());

        if (await _colorRepository.GetByIdAsync(product.ColorId) is not Color colorResult)
            return Errors.Color.NotFoundById(product.ColorId.Value.ToString());
        
        var materialsResult = await _promoCategoryRepository.GetListByIdsAsync(product.PromoCategoryIds);
        if (materialsResult.Count < product.PromoCategoryIds.Count)
            return Errors.Material.OneOrMoreNotFoundById(
                materialsResult.Select(pc => pc.Id.Value.ToString()).ToList(),
                product.PromoCategoryIds.Select(id => id.Value.ToString()).ToList());
        
        var result = new ProductResult(
            Id: product.Id.Value.ToString(),
            Name: product.Name,
            Description: product.Description,
            AverageRating: product.AverageRating.Value,
            Price: new MoneyResult(product.Price.Amount, product.Price.Currency),
            CategoryPath: categoryPath,
            PromoCategories: promoCategoriesResult.Select(p => new PromoCategoriesResult(p.Id.Value.ToString(), p.Name)).ToList(),
            Brand: new BrandResult(brandResult.Id.Value.ToString(), brandResult.Name),
            Color: new ColorResult(colorResult.Id.Value.ToString(), colorResult.Name),
            Materials: materialsResult.Select(m => new MaterialResult(m.Id.Value.ToString(), m.Name)).ToList(),
            Images: product.Images.Select(i => i.Url).ToList(),
            InStockProducts: product.InStockProducts
                .Select(p => new InStockProductResult(p.Quantity, p.Size)).ToList(),
            CreatedAt: product.CreatedAt,
            UpdatedAt: product.UpdatedAt
            );

        return result;

    }
}