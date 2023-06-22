using MeetUp.EventsService.Infrastructure.Repositories;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.Services;
using MeetUp.EventsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MeetUp.Kafka.Extensions;
using System.Reflection;
using MapsterMapper;
using Mapster;
using MeetUp.EventsService.Application.MessageHandlers;

namespace MeetUp.EventsService.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
                   this IServiceCollection services,
                   IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseLazyLoadingProxies(true)
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
                    {
                        b.MigrationsAssembly(Assembly.Load("MeetUp.EventsService.Infrastructure").FullName);
                    }));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

        public static IServiceCollection ConfigureMapster(
                   this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.Load("MeetUp.EventsService.Application"));

            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }

        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

        public static IServiceCollection ConfigureProducers(this IServiceCollection services)
        {
            services.AddKafkaProducer<string, Guid>(p =>
            {
                p.Topic = "DeletedEvents";
                p.BootstrapServers = "kafka:9092";
            });

            return services;
        }

        public static IServiceCollection ConfigureConsumers(this IServiceCollection services)
        {
            services.AddKafkaConsumer<string, DateTime, OldEventMessageHandler>(p =>
            {
                p.Topic = "OldEvents";
                p.GroupId = "OldEventsGroup";
                p.BootstrapServers = "kafka:9092";

                p.AllowAutoCreateTopics = true;
            });

            return services;
        }
    }
}
