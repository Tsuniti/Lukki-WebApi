using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence;

public class LukkiDbContext : DbContext
{
    public LukkiDbContext(DbContextOptions<LukkiDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Seller> Sellers { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(LukkiDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}