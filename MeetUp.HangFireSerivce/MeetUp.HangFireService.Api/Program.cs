using MeetUp.HangFireSerivce.Api.ExceptionHandler;
using MeetUp.HangFireSerivce.Api.Extensions;
using MeetUp.HangFireSerivce.Api.Features;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.InjectConfiguration(configuration);

var app = builder.Build();

if (Environment.GetEnvironmentVariable("INTEGRATION_TEST").IsNullOrEmpty())
{
    await app.ConfigureMigrationAsync();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

await app.InjectHangfireSettings();

app.Run();

public partial class Program { }