using System.Text.Json;
using Lukki.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lukki.Infrastructure.Persistence.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        ConfigureExchangeRatingsTable(builder);
    }

    
    private static void ConfigureExchangeRatingsTable(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ToTable("ExchangeRates");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.BaseCurrency)
            .IsRequired()
            .HasMaxLength(3)
            .IsFixedLength();


        builder.Property(x => x.Rates)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, decimal>>(v, (JsonSerializerOptions?)null) ?? new()
            )
            .Metadata.SetValueComparer(new ValueComparer<Dictionary<string, decimal>>(
                (d1, d2) => d1!.OrderBy(kv => kv.Key).SequenceEqual(d2!.OrderBy(kv => kv.Key)),
                d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
                d => d.ToDictionary(entry => entry.Key, entry => entry.Value)));


        builder.Property(x => x.LastUpdated)
            .IsRequired();
    }
}