﻿using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MeetUp.IdentityService.Tests.IntegrationTests
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
                    
                    var cacheService = services.SingleOrDefault(d =>
                        d.ServiceType.Equals(typeof(ICacheService)));

                    if (cacheService != null)
                    {
                        services.Remove(cacheService);
                    }

                    var mockCacheServvice = new Mock<ICacheService>();
                    services.AddSingleton(mockCacheServvice.Object);
                });
            });

            ConfigEnvironment();

            webHost.ConfigUserManagerFactoryAsync().Wait();

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

        private static async Task ConfigUserManagerFactoryAsync(this WebApplicationFactory<Program> webHost)
        {
            var userManager = webHost.Services.CreateScope().ServiceProvider.GetService<UserManager<IdentityUser>>();

            foreach (var user in DataFactory.GetUsers())
            {
                await userManager.CreateAsync(user, DataFactory.UserPassword);
            }
        }

        private static void ConfigEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("SECRET", "MeetUpIdentityService");
            Environment.SetEnvironmentVariable("INTEGRATION_TEST", "True");
        }
    }
}
