using Lukki.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence;

public class LukkiDbContext : DbContext
{
    public LukkiDbContext(DbContextOptions<LukkiDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(LukkiDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}