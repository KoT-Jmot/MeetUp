using MeetUp.IdentityService.Api.ExceptionHandler;
using MeetUp.IdentityService.Api.Extensions;
using MeetUp.IdentityService.Api.Features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.AddCorsPolicies(new CorsSettings
{
    UserCors = true,
    UsePolicyWithName = "CorsDefaultPolicy",
    Policies = new[]
    {
        new CorsPolicySettings
        {
            AllowAnyHeader = true,
            AllowAnyMethod = true,
            AllowedOrigins = new[] {"localhost:6001"},
            PolicyName = "CorsDefaultPolicy"
        }
    }
});

services.InjectConfigurations(configuration);

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeDbContextAsync();

app.UseRouting();


app.MapControllers();

app.UseCors("CorsDefaultPolicy");
app.Run();