using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        ConfigureCustomersTable(builder);
        ConfigureCustomerCartItems(builder);
        ConfigureCustomerInWishListProductIdsTable(builder);
        ConfigureCustomerOrderIdsTable(builder);
        ConfigureCustomerReviewIdsTable(builder);


    }

    private void ConfigureCustomerOrderIdsTable(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsMany(
            c => c.OrderIds,
            oib =>
            {
                oib.ToTable("OrderIds");
        
                oib.WithOwner().HasForeignKey("CustomerId");
        
                oib.HasKey("Id");

                oib.Property(oi => oi.Value)
                    .HasColumnName("OrderId")
                    .ValueGeneratedNever();

            });
        
        builder.Metadata.FindNavigation(nameof(Customer.OrderIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

     private void ConfigureCustomerReviewIdsTable(EntityTypeBuilder<Customer> builder)
     {
         builder.OwnsMany(
             c => c.ReviewIds,
             rib =>
             {
                 rib.ToTable("CustomerReviewIds");
     
                 rib.WithOwner().HasForeignKey("CustomerId");
     
                 rib.HasKey("Id");
     
                 rib.Property(ri => ri.Value)
                     .HasColumnName("ReviewId")
                     .ValueGeneratedNever();
     
             });
         
         builder.Metadata.FindNavigation(nameof(Customer.ReviewIds))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);
     }

     private void ConfigureCustomerInWishListProductIdsTable(EntityTypeBuilder<Customer> builder)
     {
         builder.OwnsMany(
             c => c.InWishListProductIds,
             pib =>
             {
                 pib.ToTable("CustomerInWishListProductIds");
     
                 pib.WithOwner().HasForeignKey("CustomerId");
     
                 pib.HasKey("Id");
     
                 pib.Property(p => p.Value)
                     .HasColumnName("ProductId")
                     .ValueGeneratedNever();
     
             });
         
         builder.Metadata.FindNavigation(nameof(Customer.InWishListProductIds))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);
     }
     
     private void ConfigureCustomerCartItems(EntityTypeBuilder<Customer> builder)
     {
         builder.OwnsMany(
             c => c.CartItems,
             cb =>
             {
                 cb.ToTable("CustomerCartItems");
     
                 cb.WithOwner().HasForeignKey("CustomerId");
     
                 cb.HasKey("Id");
                 
                 cb.Property(ci => ci.ProductId)
                     .HasConversion(
                         id => id.Value,
                         value => ProductId.Create(value));
     
                 cb.Property(ci => ci.Size)
                     .HasMaxLength(50);
                 
                 cb.Property(ci => ci.Quantity);
             });
         
         builder.Metadata.FindNavigation(nameof(Customer.CartItems))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);
     }

     private void ConfigureCustomersTable(EntityTypeBuilder<Customer> builder)
     {
         builder.ToTable("Customers");
         
         builder.HasKey(s => s.Id);
         
         builder.Property(s => s.Id)
             .ValueGeneratedNever()
             .HasConversion(
                 id => id.Value,
                 value => UserId.Create(value));
         
         builder.Property(s => s.FirstName)
             .HasMaxLength(100);
         builder.Property(s => s.LastName)
             .HasMaxLength(100);
         builder.Property(s => s.Email)
             .HasMaxLength(100);
         builder.Property(s => s.PasswordHash);
         builder.Property(s => s.Role)
             .HasMaxLength(100);
         builder.Property(s => s.PhoneNumber)
             .HasMaxLength(16);
         
         
         
         
     }
 }