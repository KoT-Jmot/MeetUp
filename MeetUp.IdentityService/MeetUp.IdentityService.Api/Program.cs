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

services.InjectConfigurations(configuration);

var app = builder.Build();
//AHAHAH
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("INTEGRATION_TEST")))
{
    await app.ConfigureMigrationAsync();
}

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeDbContextAsync();

app.UseRouting();

app.MapControllers();
//Hello
app.Run();
//World!
public partial class Program { }