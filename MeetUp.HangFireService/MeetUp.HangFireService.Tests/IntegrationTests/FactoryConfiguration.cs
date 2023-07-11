using MeetUp.HangFireSerivce.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeetUp.HangFireService.Tests.IntegrationTests
{
    public static class FactoryConfiguration
    {
        public static WebApplicationFactory<Program> WebApplicationFactoryConfig()
        {
            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.ConfigTestDbContext();
                });
            });

            ConfigEnvironment();

            return webHost;
        }
        private static IServiceCollection ConfigTestDbContext(this IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType.Equals(typeof(DbContextOptions<HangFireContext>)));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            return services;
        }

        private static void ConfigEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("INTEGRATION_TEST", "True");
        }
    }
}
