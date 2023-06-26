using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.HangFireSerivce.Application.Services;
using MeetUp.HangFireSerivce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MeetUp.Kafka.Extensions;
using Hangfire;

namespace MeetUp.HangFireSerivce.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
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

        public static IServiceCollection ConfigureProducers(this IServiceCollection services)
        {
            services.AddKafkaProducer<string, DateTime>(p =>
            {
                p.Topic = "OldEvents";
                p.BootstrapServers = "kafka:9092";
            });

            return services;
        }

        public static IServiceCollection InjectConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureHangFire(configuration)
                    .ConfigureProducers()
                    .ConfigureServices();

            return services;
        }
    }
}
