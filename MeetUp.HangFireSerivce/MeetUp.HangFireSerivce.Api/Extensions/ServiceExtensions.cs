using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.HangFireSerivce.Application.Services;
using MeetUp.HangFireSerivce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Hangfire;

namespace MeetUp.HangFireSerivce.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services.AddScoped<INotificationServices, NotificationServices>();

            return services;
        }

            public static IServiceCollection ConfigureHangFire(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<HangFireContext>(optionsBuilder =>
                optionsBuilder
                    .UseSqlServer(configuration.GetConnectionString("HangFireConnection")));

            services.AddHangfire(optionsBuilder =>
                optionsBuilder
                    .UseSqlServerStorage(configuration.GetConnectionString("HangFireConnection")));

            services.AddHangfireServer();

            return services;
        }
    }
}
