using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class SellerConfigurations : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        ConfigureSellersTable(builder);
        
    }

    private void ConfigureSellersTable(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .ValueGeneratedNever();
        
        builder.Property(s => s.BrandName)
            .HasMaxLength(100);
        builder.Property(s => s.FirstName)
            .HasMaxLength(100);
        builder.Property(s => s.LastName)
            .HasMaxLength(100);
        builder.Property(s => s.Email)
            .HasMaxLength(100);
        builder.Property(s => s.PasswordHash);
        builder.Property(s => s.Role)
            .HasMaxLength(100);
        
    }
}