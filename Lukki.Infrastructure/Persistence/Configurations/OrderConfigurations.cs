using Lukki.Domain.CustomerAggregate.ValueObjects;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.OrderAggregate.ValueObjects;
using Lukki.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ConfigureOrdersTable(builder);
        ConfigureOrderInOrderProductsTable(builder);

    }

    private void ConfigureOrderInOrderProductsTable(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsMany(
            o => o.InOrderProducts,
            inpb =>
            {
                inpb.ToTable("OrderInOrderProducts");

                inpb.WithOwner().HasForeignKey("OrderId");

                inpb.HasKey("Id", "OrderId");

                inpb.Property(inp => inp.Id)
                    .HasColumnName("InOrderProductId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => InOrderProductId.Create(value));

                inpb.OwnsOne(
                    inp => inp.PriceAtTimeOfOrder,
                    inpb =>
                    {
                        inpb.Property(p => p.Amount)
                            .HasPrecision(18, 2);

                        inpb.Property(p => p.Currency)
                            .HasMaxLength(3);
                    });

                inpb.Property(inp => inp.Size)
                    .HasMaxLength(50);

                inpb.Property(inp => inp.Quantity);

                inpb.Property(inp => inp.ProductId)
                    .HasConversion(
                        id => id.Value,
                        value => ProductId.Create(value));
            });
    }

    private void ConfigureOrdersTable(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));
        
        builder.Property(o => o.Status)
            .HasConversion<string>();
        
        builder.OwnsOne(o => o.TotalAmount, ob =>
        {
            ob.Property(p => p.Amount)
                .HasPrecision(18, 2);

            ob.Property(p => p.Currency)
                .HasMaxLength(3);
        });
        
        builder.OwnsOne(o => o.ShippingAddress, ob =>
        {
            ob.Property(a => a.Street)
                .HasMaxLength(200);
            ob.Property(a => a.City)
                .HasMaxLength(100);
            ob.Property(a => a.PostalCode)
                .HasMaxLength(20);
            ob.Property(a => a.Country)
                .HasMaxLength(100);
        });

        builder.OwnsOne(o => o.BillingAddress, ob =>
        {
            ob.Property(a => a.Street)
                .HasMaxLength(200);
            ob.Property(a => a.City)
                .HasMaxLength(100);
            ob.Property(a => a.PostalCode)
                .HasMaxLength(20);
            ob.Property(a => a.Country)
                .HasMaxLength(100);
        });
        
        builder.Property(o => o.CustomerId)
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value));
        
        /*builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);*/
        
    }
}