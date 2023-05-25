using MeetUp.IdentityService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MeetUp.IdentityService.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureSqlServer(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
            {
                b.MigrationsAssembly(Assembly.Load("MeetUp.IdentityService.Infrastructure").FullName);
            }));

        return services;
    }
}