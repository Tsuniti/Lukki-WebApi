using Lukki.Domain.CategoryAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
        ConfigureProductImagesTable(builder);
        ConfigureProductInStockProductsTable(builder);
        ConfigureProductReviewIdsTable(builder);

    }

    private void ConfigureProductReviewIdsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(
            p => p.ReviewIds,
            rib =>
            {
                rib.ToTable("ProductReviewIds");

                rib.WithOwner().HasForeignKey("ProductId");

                rib.HasKey("Id");

                rib.Property(p => p.Value)
                    .HasColumnName("ReviewId")
                    .ValueGeneratedNever();

            });
        
        builder.Metadata.FindNavigation(nameof(Product.ReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProductInStockProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(
            p => p.InStockProducts,
            pb =>
            {
                pb.ToTable("ProductInStockProducts");

                pb.WithOwner().HasForeignKey("ProductId");

                pb.HasKey("Id");

                pb.Property(inp => inp.Quantity);
                pb.Property(inp => inp.Size);
            });
        
        builder.Metadata.FindNavigation(nameof(Product.InStockProducts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProductImagesTable(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsMany(
            p => p.Images,
            pb =>
            {
                pb.ToTable("ProductImages");

                pb.WithOwner().HasForeignKey("ProductId");

                pb.HasKey("Id");

                pb.Property(i => i.Url); 
            });
        
        builder.Metadata.FindNavigation(nameof(Product.Images))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }

    private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));
        
        
        builder.HasIndex(p => p.TargetGroup);



        builder.Property(p => p.Name)
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .HasMaxLength(200);
        builder.Property(p => p.TargetGroup)
            .HasConversion<string>();

        builder.OwnsOne(p => p.AverageRating, pb =>
        {
            pb.Property(p => p.Value)
                .HasPrecision(2, 1);
            
            pb.Property(p => p.NumRatings)
                .HasDefaultValue(0);
        });
        
        builder.OwnsOne(p => p.Price, pb =>
        {
            pb.Property(p => p.Amount)
                .HasPrecision(18, 2);

            pb.Property(p => p.Currency)
                .HasMaxLength(3);
        });
        
        
        builder.Property(p => p.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));
        
        builder.Property(p => p.SellerId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
        
    }
}