﻿using MeetUp.CommentsService.Infrastructure;
using MeetUp.CommentsService.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeetUp.CommentsService.Tests.IntegrationTests
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

            webHost.ConfigRpositoryManagerFactoryAsync().Wait();

            return webHost;
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

            foreach (var comment in DataFactory.GetComments())
            {
                await context.Comments.AddAsync(comment);
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
