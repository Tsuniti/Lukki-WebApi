using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.ReviewAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        ConfigureReviewsTable(builder);
    }

    private void ConfigureReviewsTable(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ReviewId.Create(value));

        builder.Property(r => r.Rating);
        builder.Property(r => r.Comment)
            .HasMaxLength(1000);
        
        builder.Property(r => r.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));
        
        /*builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);*/
        
        
        builder.Property(r => r.CustomerId)
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));
        
        /*builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);*/
        

    }
}