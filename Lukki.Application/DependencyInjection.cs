using System.Reflection;
using FluentValidation;
using Lukki.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Lukki.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        //ValidatorOptions.Global.LanguageManager.Enabled = false; // Disable global language manager
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}