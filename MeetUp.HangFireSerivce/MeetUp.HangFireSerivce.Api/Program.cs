using Hangfire;
using MeetUp.HangFireSerivce.Api.ExceptionHandler;
using MeetUp.HangFireSerivce.Api.Extensions;
using MeetUp.HangFireSerivce.Api.Features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.ConfigureHangFire(configuration)
        .ConfigureProducers()
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeHangFireContextAsync();

app.UseRouting();

app.UseHangfireDashboard(options: new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

await app.InitializeHangFireJobStorageAsync();


app.Run();
