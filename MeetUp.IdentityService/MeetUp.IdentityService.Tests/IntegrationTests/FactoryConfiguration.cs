using Microsoft.AspNetCore.Mvc.Testing;

namespace MeetUp.IdentityService.Tests.IntegrationTests
{
    public static class FactoryConfiguration
    {
        public static WebApplicationFactory<Program> WebApplicationFactoryConfig(this WebApplicationFactory<Program> webHost)
        {
            webHost.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            });

            return webHost;
        }
    }
}
