using MeetUp.EventsService.Infrastructure;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.Kafka.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MeetUp.EventsService.Tests.IntegrationTests
{
    public static class FactoryConfiguration
    {
        public static WebApplicationFactory<Program> WebApplicationFactoryConfig()
        {
            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ConfigTestDbContext()
                            .ConfigTestKafkaProducer();
                });
            });

            ConfigEnvironment();

            webHost.ConfigRpositoryManagerFactoryAsync().Wait();

            return webHost;
        }

        private static IServiceCollection ConfigTestKafkaProducer(this IServiceCollection services)
        {
            var kafkaProducer = services.SingleOrDefault(d =>
                        d.ServiceType.Equals(typeof(IKafkaProducer<string, Guid>)));

            if (kafkaProducer != null)
            {
                services.Remove(kafkaProducer);
            }

            var kafkaProducerMock = new Mock<IKafkaProducer<string, Guid>>();

            services.AddSingleton(kafkaProducerMock.Object);

            return services;
        }

        private static IServiceCollection ConfigTestDbContext(this IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType.Equals(typeof(DbContextOptions<ApplicationContext>)));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContextPool<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            return services;
        }
        private static async Task ConfigRpositoryManagerFactoryAsync(this WebApplicationFactory<Program> webHost)
        {
            var context = webHost.Services.CreateScope().ServiceProvider.GetService<IRepositoryManager>();

            foreach (var category in DataFactory.GetAllCategoryEntity())
            {
                await context.Categories.AddAsync(category);
            }

            foreach (var eventEntity in DataFactory.GetEvents())
            {
                await context.Events.AddAsync(eventEntity);
            }

            await context.SaveChangesAsync();
        }

        private static void ConfigEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("INTEGRATION_TEST", "True");
        }
    }
}
