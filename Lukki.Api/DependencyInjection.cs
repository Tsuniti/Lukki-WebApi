using Lukki.Api.Common.Errors;
using Lukki.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Lukki.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
    
        services.AddSingleton<ProblemDetailsFactory, LukkiProblemDetailsFactory>();
        
        services.AddMappings();
        return services;
    }
}