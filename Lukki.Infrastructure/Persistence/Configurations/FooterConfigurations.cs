using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.FooterAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class FooterConfigurations : IEntityTypeConfiguration<Footer>
{
    public void Configure(EntityTypeBuilder<Footer> builder)
    {
        ConfigureFootersTable(builder);
        ConfigureFooterSectionsTable(builder);
    }

    private void ConfigureFooterSectionsTable(EntityTypeBuilder<Footer> builder)
    {
        builder.OwnsMany(
            f => f.Sections,
            sb =>
            {
                sb.ToTable("FooterSections");

                sb.WithOwner().HasForeignKey("FooterId");

                sb.HasKey("Id");

                sb.Property(s => s.Name)
                    .HasMaxLength(100);
                sb.Property(s => s.SortOrder);


                sb.OwnsMany(
                    s => s.Links,
                    lb =>
                    {
                        lb.ToTable("FooterLinks");
                        lb.WithOwner().HasForeignKey( "FooterSectionId", "FooterId");

                        lb.Property<int>("Id");
                        lb.HasKey("Id");
                        
                        lb.HasOne<FooterSection>()
                            .WithMany(s => s.Links)
                            .HasForeignKey("FooterSectionId", "FooterId")
                            .HasPrincipalKey("Id", "FooterId");


                        lb.Property(l => l.Text)
                            .HasMaxLength(100);
                        lb.Property(l => l.Url);
                        lb.Property(l => l.Icon)
                            .HasConversion(
                                i => i.Url,
                                url => Image.Create(url));
                        lb.Property(l => l.SortOrder);

                    });
                sb.Navigation(s => s.Links).Metadata.SetField("_links");
                sb.Navigation(s => s.Links).UsePropertyAccessMode(PropertyAccessMode.Field);

            });
        
        builder.Metadata.FindNavigation(nameof(Footer.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }

    private void ConfigureFootersTable(EntityTypeBuilder<Footer> builder)
    {
        builder.ToTable("Footers");
        
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => FooterId.Create(value));
        
        
        builder.HasIndex(f => f.Name);

        builder.Property(p => p.Name)
            .HasMaxLength(100);
        
        builder.Property(p => p.CopyrightText)
            .HasMaxLength(200);
    }
}