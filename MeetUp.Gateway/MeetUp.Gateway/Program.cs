using MeetUp.Gateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

services.ConfigureJWT(configuration)
        .AddOcelot(configuration);

var app = builder.Build();


await app.UseOcelot();

app.UseAuthentication()
   .UseAuthorization();


app.Run();
