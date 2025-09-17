using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Products.Common;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LukkiDbContext _dbContext;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    public ProductRepository(LukkiDbContext dbContext, IExchangeRateRepository exchangeRateRepository)
    {
        _dbContext = dbContext;
        _exchangeRateRepository = exchangeRateRepository;
    }

    public async Task AddAsync(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Product> AddRating(Product product, short rating)
    {
        product.AverageRating.AddNewRating(rating);
        return Update(product);
    }

    public async Task<List<Product>> GetListByProductIdsAsync(IEnumerable<ProductId> productIds)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
    }

    public async Task<(IReadOnlyList<Product> Products, int TotalItems)> GetPagedAsync(ProductFilter filter)
    {
        IQueryable<Product> query = _dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.ToLower();
            query = query.Where(
                p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Description.ToLower().Contains(term));
        }

        if (filter.MinPrice.HasValue || filter.MaxPrice.HasValue)
        {
            var exchangeRates = await _exchangeRateRepository.GetAsync()
                                ?? throw new InvalidOperationException("Exchange rate failed.");

            if (!exchangeRates.Rates.TryGetValue(filter.Currency, out var currencyRate))
            {
                throw new InvalidOperationException($"Exchange rate failed.");
            }

            if (filter.MinPrice.HasValue)
            {
                var minInBase = filter.MinPrice.Value / currencyRate;
                query = query.Where(p => p.Price.Amount >= minInBase);
            }

            if (filter.MaxPrice.HasValue)
            {
                var maxInBase = filter.MaxPrice.Value / currencyRate;
                query = query.Where(p => p.Price.Amount <= maxInBase);
            }
        }
        

        if (filter.CategoryIds is { Count: > 0 })
        {
            var categoryTrees = await BuildCategoryTreesAsync();
    
            // 1. Find all categories that contain any of Filter.categoryids in their hierarchy
            var allValidCategoryIds = new HashSet<CategoryId>();
    
            foreach (var category in categoryTrees)
            {
                // Check whether the tree contains any of the filter categories
                if (category.Value.Any(treeCategoryId => filter.CategoryIds.Contains(treeCategoryId)))
                {
                    allValidCategoryIds.Add(category.Key);
                }
            }
    
            // 2. Also add the filter categories themselves (in case the product is tied directly)
            foreach (var filterCategoryId in filter.CategoryIds)
            {
                allValidCategoryIds.Add(filterCategoryId);
            }
            
            query = query.Where(p => allValidCategoryIds.Contains(p.CategoryId));
        }


        if (filter.PromoCategoryIds is { Count: > 0 })
            query = query.Where(
                p => p.PromoCategoryIds.Any(promoCategoryId => filter.PromoCategoryIds.Contains(promoCategoryId)));

        if (filter.BrandIds is { Count: > 0 })
            query = query.Where(p => filter.BrandIds.Contains(p.BrandId));

        if (filter.ColorIds is { Count: > 0 })
            query = query.Where(p => filter.ColorIds.Contains(p.ColorId));

        if (filter.MaterialIds is { Count: > 0 })
            query = query.Where(p => p.MaterialIds.Any(materialId => filter.MaterialIds.Contains(materialId)));

        query = filter.SortBy switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "newest" => query.OrderByDescending(p => p.CreatedAt),
            "best_selling" => query.OrderByDescending(p => p.AverageRating),
            _ => query.OrderBy(p => p.Id) // relevance or default
        };

        var totalCount = await query.CountAsync();

        var products = await query
            .Skip((filter.PageNumber - 1) * filter.ItemsPerPage)
            .Take(filter.ItemsPerPage)
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<Product?> GetByIdAsync(ProductId id)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> Update(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }


    private async Task<Dictionary<CategoryId, List<CategoryId>>> BuildCategoryTreesAsync()
    {
        var allCategories = await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync();
        
        return allCategories.ToDictionary(
            c => c.Id,
            c => GetCategoryTreeSync(c.Id, allCategories)
        );
    }
    
    private List<CategoryId> GetCategoryTreeSync(CategoryId categoryId, List<Category> allCategories)
    {
        var result = new List<CategoryId>();
        var current = categoryId;
        var depth = 0;
        const int maxDepth = 20; // Infinite cycle protection
        
        
        result.Add(current);
        
        while (current is not null && depth < maxDepth)
        {
            var category = allCategories.FirstOrDefault(c => c.Id == current);
            if (category?.ParentId is null) break;
        
            result.Add(category.ParentId);
            current = category.ParentId;
            depth++;
        }
    
        return result;
    }
}