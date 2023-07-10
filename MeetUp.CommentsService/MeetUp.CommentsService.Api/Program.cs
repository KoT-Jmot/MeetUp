using FluentValidation;
using MeetUp.CommentsService.Api.ExceptionHandler;
using MeetUp.CommentsService.Api.Extensions;
using MeetUp.CommentsService.Api.Features;
using MeetUp.CommentsService.Application.Hubs;
using Microsoft.IdentityModel.Tokens;
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

services.ConfigureConsumers();

services.ConfigureMapster()
        .ConfigureGRPC(configuration)
        .ConfigureSignalR()
        .ConfigureServices();

var app = builder.Build();

if (Environment.GetEnvironmentVariable("INTEGRATION_TEST").IsNullOrEmpty())
{
    await app.ConfigureMigrationAsync();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.MapHub<CommentsHub>("/chat");
app.MapControllers();

app.Run();

public partial class Program { }