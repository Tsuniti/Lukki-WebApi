using Lukki.Domain.ColorAggregate;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ColorConfigurations : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        ConfigureColorsTable(builder);
    }

    private void ConfigureColorsTable(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("Colors");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ColorId.Create(value));
        
        

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.HexColorCode)
            .HasMaxLength(7);
        
        builder.Property(c => c.Icon)
            .IsRequired(false)
            .HasConversion(
                image => image.Url,
                url => Image.Create(url));
    }
}