using MeetUp.EventsService.Infrastructure;
using MeetUp.EventsService.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MeetUp.EventsService.Tests.IntegrationTests.CategoriesTests
{
    public static class FactoryConfiguration
    {
        public static WebApplicationFactory<Program> WebApplicationFactoryConfig()
        {
            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
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
                });
            });

            ConfigEnvironment();

            webHost.ConfigUserManagerFactoryAsync().Wait();

            return webHost;
        }

        private static async Task ConfigUserManagerFactoryAsync(this WebApplicationFactory<Program> webHost)
        {
            var context = webHost.Services.CreateScope().ServiceProvider.GetService<IRepositoryManager>();

            foreach (var category in DataFactory.GetAllCategoryEntity())
            {
                await context.Categories.AddAsync(category);
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
