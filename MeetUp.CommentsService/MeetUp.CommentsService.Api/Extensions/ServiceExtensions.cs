using MeetUp.CommentsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MeetUp.CommentsService.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b =>
                    {
                        b.MigrationsAssembly(Assembly.Load("MeetUp.CommentsService.Infrastructure").FullName);
                    }));

            return services;
        }
    }
}
