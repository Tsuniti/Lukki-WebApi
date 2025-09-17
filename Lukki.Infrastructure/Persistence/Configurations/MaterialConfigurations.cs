using Lukki.Domain.MaterialAggregate;
using Lukki.Domain.MaterialAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class MaterialConfigurations : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        ConfigureMaterialsTable(builder);
    }

    private void ConfigureMaterialsTable(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MaterialId.Create(value));
        
        builder.Property(c => c.Name)
            .HasMaxLength(100);

    }
}