using MeetUp.CommentsService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureSqlServer(configuration)
        .AddControllers();

var app = await builder.Build().ConfigureMigrationAsync();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
