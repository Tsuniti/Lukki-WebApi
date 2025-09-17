using Lukki.Domain.ReviewBannerAggregate;
using Lukki.Domain.ReviewBannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ReviewBannerConfigurations : IEntityTypeConfiguration<ReviewBanner>
{
    public void Configure(EntityTypeBuilder<ReviewBanner> builder)
    {
        ConfigureReviewBannersTable(builder); 
        ConfigureReviewBannerReviewIdsTable(builder);
    }

    private void ConfigureReviewBannersTable(EntityTypeBuilder<ReviewBanner> builder)
    {
        
        
        builder.ToTable("ReviewBanners");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ReviewBannerId.Create(value));
        
        
        
        builder.Property(b => b.Title)
            .HasMaxLength(100);
        
    }
    
    private void ConfigureReviewBannerReviewIdsTable(EntityTypeBuilder<ReviewBanner> builder)
    {
        builder.OwnsMany(
            b => b.ReviewIds,
            bb =>
            {
                bb.ToTable("ReviewBannerReviewIds");
     
                bb.WithOwner().HasForeignKey("ReviewBannerId");
     
                bb.HasKey("Id");
     
                bb.Property(p => p.Value)
                    .HasColumnName("ReviewId")
                    .ValueGeneratedNever();
                 
                /*builder.HasOne<Review>()
                    .WithMany()
                    .HasForeignKey("ReviewId")
                    .OnDelete(DeleteBehavior.Cascade);*/

     
            });
         
        builder.Metadata.FindNavigation(nameof(ReviewBanner.ReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}