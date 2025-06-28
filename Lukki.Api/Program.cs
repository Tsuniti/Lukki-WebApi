using Lukki.Api.Common.Errors;
using Lukki.Application;
using Lukki.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddControllers();
    
    builder.Services.AddSingleton<ProblemDetailsFactory, LukkiProblemDetailsFactory>();
}


var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
