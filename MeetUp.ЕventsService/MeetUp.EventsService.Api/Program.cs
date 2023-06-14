using MeetUp.EventsService.Api.ExceptionHandler;
using MeetUp.EventsService.Api.Extensions;
using MeetUp.EventsService.Api.Features;
using System.Reflection;
using FluentValidation;
using Serilog;
using MeetUp.EventsService.Application.Services;

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

services.AddValidatorsFromAssembly(Assembly.Load("MeetUp.EventsService.Application"));

services.AddGrpc();

services.ConfigureMapster()
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGrpcService<GreeterService>();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
