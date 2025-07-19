using Lukki.Domain.BannerAggregate;
using Lukki.Domain.BannerAggregate.ValueObjects;
using Lukki.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class BannerConfigurations : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        ConfigureBannersTable(builder); 
        ConfigureBannerSlidesTable(builder);
    }

    private void ConfigureBannersTable(EntityTypeBuilder<Banner> builder)
    {
        
        
        builder.ToTable("Banners");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => BannerId.Create(value));
        
        

        builder.Property(b => b.Name)
            .HasMaxLength(100);

        
    }
    
    private void ConfigureBannerSlidesTable(EntityTypeBuilder<Banner> builder)
    {
        builder.OwnsMany(
            b => b.Slides,
            bb =>
            {
                bb.ToTable("BannerSlides");

                bb.WithOwner().HasForeignKey("BannerId");

                bb.HasKey("Id");

                bb.Property(s => s.Image)
                    .HasConversion(
                        i => i.Url,
                        url => Image.Create(url));

                bb.Property(s => s.Text)
                    .HasMaxLength(200);

                bb.Property(s => s.ButtonText)
                    .HasMaxLength(100);
                bb.Property(s => s.ButtonUrl);

                bb.Property(s => s.SortOrder);


            });
        
        builder.Metadata.FindNavigation(nameof(Banner.Slides))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }
}