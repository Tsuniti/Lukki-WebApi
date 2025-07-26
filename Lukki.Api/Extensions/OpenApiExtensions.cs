using Scalar.AspNetCore;

namespace Lukki.Api.Extensions;

public static class OpenApiExtensions
{
    public static void UseDefaultOpenApi(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            
            var context = app.Services.GetService<IHttpContextAccessor>()?.HttpContext;
            var host = context?.Request.Host ?? new HostString("localhost");
            
            
            var scheme = context?.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() 
                         ?? context?.Request.Scheme 
                         ?? "https";

            options
                .WithTitle("Workout Service API")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithDefaultHttpClient(
                    ScalarTarget.CSharp,
                    ScalarClient.HttpClient)
                .WithBaseServerUrl($"{scheme}://{host}");
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
    }
}