using MeetUp.EventsService.Api.ExceptionHandler;
using MeetUp.EventsService.Api.Extensions;
using MeetUp.EventsService.Api.Features;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.ConfigureSqlServer(configuration)
        .AddControllers();

services.ConfigureJWT(configuration)
        .ConfigureMapster()
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
