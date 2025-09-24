using System.Reflection;
using FluentValidation;
using Lukki.Application.Common.Behaviors;
using Lukki.Application.Common.Interfaces.Services.Currency;
using Lukki.Application.Services.Currency;
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
        
        
        services.AddScoped<IExchangeRateService, ExchangeRateService>();
        //services.AddScoped<ICurrencyConverter, CurrencyConverter>();


        ValidatorOptions.Global.LanguageManager.Enabled = false; // Disable global language manager
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}