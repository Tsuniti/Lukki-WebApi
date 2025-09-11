using Lukki.Api;
using Lukki.Api.Extensions;
using Lukki.Application;
using Lukki.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddOpenApi(options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); })
        .AddHttpContextAccessor()
        .AddCors(
            options =>
            {
                options.AddPolicy(
                    "AllowFrontend",
                    policy => policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
            });
    ;


    builder.Services.Configure<ForwardedHeadersOptions>(
        options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });
}


var app = builder.Build();
{
    app.UseForwardedHeaders();
    if (!app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseExceptionHandler("/error");

    app.UseDefaultOpenApi();

    app.UseForwardedHeaders();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors("AllowFrontend");
    app.Run();
}