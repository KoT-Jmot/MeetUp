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
            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });

            ConfigEnvironment();

            return webHost;
        }

        private static void ConfigEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("INTEGRATION_TEST", "True");
        }
    }
}
