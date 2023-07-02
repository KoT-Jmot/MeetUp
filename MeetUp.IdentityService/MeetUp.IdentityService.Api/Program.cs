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

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeDbContextAsync();

app.UseRouting();

app.MapControllers();

app.Run();

public partial class Program { }