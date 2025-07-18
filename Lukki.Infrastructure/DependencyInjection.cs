using System.Text;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.External;
using Lukki.Infrastructure.External.ExchangeRateApi;
using Lukki.Infrastructure.Persistence;
using Lukki.Infrastructure.Persistence.Interceptors;
using Lukki.Infrastructure.Persistence.Repositories;
using Lukki.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lukki.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
            .AddPersistance();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services)
    {
        services.AddDbContext<LukkiDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=Lukki;User Id=sa;Password=lukki123!;Encrypt=false"));
        
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton(Options.Create(JwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        
                        ValidIssuer = JwtSettings.Issuer,
                        ValidAudience = JwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(JwtSettings.Secret))
                    };
                });
        return services;
    }
    
    public static IServiceCollection AddExchangeRateApi(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var ExchangeRateApiSettings = new ExchangeRateApiSettings();
        configuration.Bind(ExchangeRateApiSettings.SectionName, ExchangeRateApiSettings);
        services.Configure<ExchangeRateApiSettings>(configuration.GetSection(ExchangeRateApiSettings.SectionName));
        
        services.AddSingleton(Options.Create(ExchangeRateApiSettings));
        services.AddHttpClient<IExchangeRateApiClient, ExchangeRateApiClient>();

        
        return services;
    }
}