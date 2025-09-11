using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ProductBannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ProductBannerConfigurations : IEntityTypeConfiguration<ProductBanner>
{
    public void Configure(EntityTypeBuilder<ProductBanner> builder)
    {
        ConfigureProductBannersTable(builder); 
        ConfigureProductBannerProductIdsTable(builder);
    }

    private void ConfigureProductBannersTable(EntityTypeBuilder<ProductBanner> builder)
    {
        
        
        builder.ToTable("ProductBanners");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductBannerId.Create(value));
        
        
        
        builder.Property(b => b.Title)
            .HasMaxLength(100);
        
    }
    
    private void ConfigureProductBannerProductIdsTable(EntityTypeBuilder<ProductBanner> builder)
    {
        builder.OwnsMany(
            b => b.ProductIds,
            bb =>
            {
                bb.ToTable("ProductBannerProductIds");
     
                bb.WithOwner().HasForeignKey("ProductBannerId");
     
                bb.HasKey("Id");
     
                bb.Property(p => p.Value)
                    .HasColumnName("ProductId")
                    .ValueGeneratedNever();
                 
                /*builder.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade);*/

     
            });
         
        builder.Metadata.FindNavigation(nameof(ProductBanner.ProductIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}