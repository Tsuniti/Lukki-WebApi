using Lukki.Domain.PromoCategoryAggregate;
using Lukki.Domain.PromoCategoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class PromoCategoryConfigurations : IEntityTypeConfiguration<PromoCategory>
{
    public void Configure(EntityTypeBuilder<PromoCategory> builder)
    {
        ConfigurePromoCategoriesTable(builder);
    }

    private void ConfigurePromoCategoriesTable(EntityTypeBuilder<PromoCategory> builder)
    {
        builder.ToTable("PromoCategories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PromoCategoryId.Create(value));
        
        builder.Property(c => c.Name)
            .HasMaxLength(100);

    }
}