var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();