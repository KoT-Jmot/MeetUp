using MeetUp.IdentityService.Api.ExceptionHandler;
using MeetUp.IdentityService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureSqlServer(configuration)
        .AddControllers();

services.ConfigureIdentity()
        .ConfigureJWT(configuration)
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.InitializeDbContextAsync();

app.UseHttpsRedirection()
   .UseRouting();

app.MapControllers();

app.Run();