using Lukki.Domain.BannerAggregate;
using Lukki.Domain.CategoryAggregate;
using Lukki.Domain.Common.Models;
using Lukki.Domain.CustomerAggregate;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.HeaderAggregate;
using Lukki.Domain.OrderAggregate;
using Lukki.Domain.ProductAggregate;
using Lukki.Domain.ProductBannerAggregate;
using Lukki.Domain.ReviewAggregate;
using Lukki.Domain.SellerAggregate;
using Lukki.Domain.TextboxBannerAggregate;
using Lukki.Infrastructure.Persistence.Interceptors;
using Lukki.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Lukki.Infrastructure.Persistence;

public class LukkiDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public LukkiDbContext(DbContextOptions<LukkiDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Seller> Sellers { get; set; } = null!;
    public DbSet<Banner> Banners { get; set; } = null!;
    public DbSet<TextboxBanner> TextboxBanners { get; set; } = null!;
    public DbSet<ProductBanner> ProductBanners { get; set; } = null!;
    public DbSet<Footer> Footers { get; set; } = null!;
    public DbSet<MyHeader> Headers { get; set; } = null!;

    public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(LukkiDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}