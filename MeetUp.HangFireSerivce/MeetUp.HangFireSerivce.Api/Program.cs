using Hangfire;
using MeetUp.HangFireSerivce.Api.ExceptionHandler;
using MeetUp.HangFireSerivce.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureHangFire(configuration)
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeHangFireContextAsync();

app.UseRouting();

app.UseHangfireDashboard();

await app.InitializeHangFireJobStorageAsync();


app.Run();
