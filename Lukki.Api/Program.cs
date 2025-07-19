using Lukki.Api;
using Lukki.Api.Extensions;
using Lukki.Application;
using Lukki.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration)

        .AddOpenApi();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseDefaultOpenApi();
    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
