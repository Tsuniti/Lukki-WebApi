using Lukki.Application.Common.Interfaces.Authentication;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Application.Common.Interfaces.Services;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Persistence;
using Lukki.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lukki.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}