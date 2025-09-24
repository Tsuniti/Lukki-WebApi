using Lukki.Contracts.ProductBanners;
using Lukki.Contracts.Products;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.ProductAggregate;

namespace Lukki.Api.Common.Mapping.Services;

static class ProductBannerMappingService
{
    public static List<GroupedProduct> GroupProductsByCategories(List<Product> products, List<Category> categories)
    {
        var rootCategories = categories.Where(c => c.ParentId == null).ToList();
        var groupedProducts = new List<GroupedProduct>();
        foreach (var category in rootCategories)
        {
            groupedProducts.Add(new GroupedProduct (category.Name, new List<CreateProductResponse>()));
        }

        foreach (var product in products)
        {
            var loopSubCategoryId = product.CategoryId;
            Category parentCategory = null;
            for(int i = 0; i < 100; i++) // Limit to 100 iterations to avoid potential infinite loop
            {
                var subCategory = categories.FirstOrDefault(c => c.Id == loopSubCategoryId);
               loopSubCategoryId = subCategory.ParentId;
               if (loopSubCategoryId is null)
               {
                   parentCategory = categories.FirstOrDefault(c => c.Id == subCategory.Id);
                   break;
               }
            }
            foreach (var groupedProduct in groupedProducts)
            {
                if (groupedProduct.GroupName == parentCategory.Name)
                {
                    groupedProduct.Products.Add(new CreateProductResponse(
                        Id: product.Id.Value.ToString(),
                        Name: product.Name,
                        Description: product.Description,
                        AverageRating: product.AverageRating.Value,
                        Price: new CreateMoneyResponse(
                            Amount: product.Price.Amount,
                            Currency: product.Price.Currency
                        ),
                        CategoryId: product.CategoryId.Value.ToString(),
                        PromoCategoryIds: product.PromoCategoryIds.Select(pc => pc.Value.ToString()).ToList(),
                        BrandId: product.BrandId.Value.ToString(),
                        ColorId: product.ColorId.Value.ToString(),
                        MaterialIds: product.MaterialIds.Select(m => m.Value.ToString()).ToList(),
                        Images: product.Images.Select(i => i.Url).ToList(),
                        InStockProducts: product.InStockProducts.Select(inStock => new CreateInStockProductResponse(
                            Quantity: inStock.Quantity,
                            Size: inStock.Size
                        )).ToList(),
                        CreatedAt: product.CreatedAt,
                        UpdatedAt: product.UpdatedAt
                    ));
                }
            }
            
        }

        return groupedProducts;
    }
}