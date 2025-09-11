using System.Text;
using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.External.ExchangeRateApi;
using Lukki.Infrastructure.External.Cloudinary;
using Lukki.Infrastructure.OtherSettings;
using Lukki.Infrastructure.Persistence;
using Lukki.Infrastructure.Persistence.Interceptors;
using Lukki.Infrastructure.Persistence.Repositories;
using Lukki.Infrastructure.Services;
using Lukki.Infrastructure.Services.ImageCompressor;
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
            .AddExchangeRateApi(configuration)
            .AddCloudinaryImageService(configuration)
            .AddDbContext(configuration)
            .AddPersistance();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services)
    {
        


        services.AddScoped<IImageCompressor, ImageCompressor>();
        
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

        //services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ITextboxBannerRepository, TextboxBannerRepository>();
        services.AddScoped<IProductBannerRepository, ProductBannerRepository>();
        services.AddScoped<IFooterRepository, FooterRepository>();
        services.AddScoped<IHeaderRepository, HeaderRepository>();
        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        var DBSettings = new DBSettings();
        configuration.Bind(DBSettings.SectionName, DBSettings);
        services.Configure<DBSettings>(configuration.GetSection(DBSettings.SectionName));

        services.AddDbContext<LukkiDbContext>( options =>
        {
            options.UseNpgsql(DBSettings.ConnectionString);
        });
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
        services.Configure<ExchangeRateApiSettings>(
            configuration.GetSection(ExchangeRateApiSettings.SectionName));
    
        services.AddHttpClient<IExchangeRateApiClient, ExchangeRateApiClient>();
    
        return services;
    }
    
    public static IServiceCollection AddCloudinaryImageService(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.SectionName));
        services.AddSingleton<IImageStorageService, CloudinaryImageService /*LocalImageStorageService*/>();
        return services;
    }
    
}