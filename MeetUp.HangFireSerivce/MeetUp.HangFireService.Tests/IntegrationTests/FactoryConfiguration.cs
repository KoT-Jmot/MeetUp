using Microsoft.AspNetCore.Mvc.Testing;

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
