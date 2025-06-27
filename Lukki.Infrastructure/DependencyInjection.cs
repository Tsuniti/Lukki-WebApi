using Lukki.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Lukki.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}