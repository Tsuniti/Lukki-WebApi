using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.HeaderAggregate;
using Lukki.Domain.HeaderAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class HeaderConfigurations : IEntityTypeConfiguration<MyHeader>
{
    public void Configure(EntityTypeBuilder<MyHeader> builder)
    {
        ConfigureHeadersTable(builder);
        ConfigureHeaderBurgerMenuLinksTable(builder);
        ConfigureHeaderIconButtonsTable(builder);
    }

    private void ConfigureHeaderIconButtonsTable(EntityTypeBuilder<MyHeader> builder)
    {
        builder.OwnsMany(
            h => h.Buttons,
            bb =>
            {
                bb.ToTable("HeaderIconButtons");

                bb.WithOwner().HasForeignKey("HeaderId");

                bb.HasKey("Id");
                
                bb.Property(b => b.Icon)
                    .HasConversion(
                        i => i.Url,
                        url => Image.Create(url));

                bb.Property(l => l.Url);
                bb.Property(s => s.SortOrder);


            });
        
        builder.Metadata.FindNavigation(nameof(MyHeader.BurgerMenuLinks))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureHeaderBurgerMenuLinksTable(EntityTypeBuilder<MyHeader> builder)
    {
        builder.OwnsMany(
            h => h.BurgerMenuLinks,
            lb =>
            {
                lb.ToTable("HeaderBurgerMenuLinks");

                lb.WithOwner().HasForeignKey("HeaderId");

                lb.HasKey("Id");
                
                lb.Property(l => l.Text)
                    .HasMaxLength(100);

                lb.Property(l => l.Url);
                lb.Property(s => s.SortOrder);


            });
        
        builder.Metadata.FindNavigation(nameof(MyHeader.BurgerMenuLinks))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureHeadersTable(EntityTypeBuilder<MyHeader> builder)
    {
        builder.ToTable("Headers");
        
        builder.HasKey(h => h.Id);
        
        builder.Property(h => h.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HeaderId.Create(value));
        
        
        builder.HasIndex(h => h.Name);

        builder.Property(h => h.Name)
            .HasMaxLength(100);
        
        builder.Property(h => h.Logo)
            .HasConversion(
                i => i.Url,
                url => Image.Create(url));
        builder.Property(h => h.OnHoverLogo)
            .HasConversion(
                i => i.Url,
                url => Image.Create(url));
        
    }
}