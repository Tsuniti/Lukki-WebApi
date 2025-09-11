using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Domain.TextboxBannerAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class TextboxBannerConfigurations : IEntityTypeConfiguration<TextboxBanner>
{
    public void Configure(EntityTypeBuilder<TextboxBanner> builder)
    {
        ConfigureTextboxBannersTable(builder); 
    }

    private void ConfigureTextboxBannersTable(EntityTypeBuilder<TextboxBanner> builder)
    {
        
        
        builder.ToTable("TextboxBanners");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TextboxBannerId.Create(value));
        
        

        builder.Property(b => b.Name)
            .HasMaxLength(100);
        builder.Property(b => b.Text)
            .HasMaxLength(500);
        builder.Property(b => b.Description)
            .HasMaxLength(1000);
        builder.Property(b => b.Placeholder)
            .HasMaxLength(200);
        builder.Property(b => b.ButtonText)
            .HasMaxLength(100);
        builder.Property(b => b.Background)
            .HasConversion(
                i => i.Url,
                url => Image.Create(url));
        
    }
    
}