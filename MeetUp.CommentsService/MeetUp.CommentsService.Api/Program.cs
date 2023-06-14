using FluentValidation;
using MeetUp.CommentsService.Api.ExceptionHandler;
using MeetUp.CommentsService.Api.Extensions;
using MeetUp.CommentsService.Api.Features;
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

services.AddValidatorsFromAssembly(Assembly.Load("MeetUp.CommentsService.Application"));

services.ConfigureMapster(configuration)
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
