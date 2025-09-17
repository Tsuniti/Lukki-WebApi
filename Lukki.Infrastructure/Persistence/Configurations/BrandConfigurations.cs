using Lukki.Domain.BrandAggregate;
using Lukki.Domain.BrandAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class BrandConfigurations : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        ConfigureBrandsTable(builder);
    }

    private void ConfigureBrandsTable(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => BrandId.Create(value));
        
        

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Logo)
            .HasConversion(
                image => image.Url,
                url => Image.Create(url));

    }
}