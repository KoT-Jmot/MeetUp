using MeetUp.IdentityService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MeetUp.IdentityService.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureSqlServer(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}