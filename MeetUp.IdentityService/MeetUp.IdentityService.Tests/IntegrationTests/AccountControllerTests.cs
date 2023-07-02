using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeetUp.IdentityService.Tests.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            factory = factory.WebApplicationFactoryConfig();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        }

        [Theory]
        [InlineData("/account")]
        public async Task GetAllUsersAsync_SendRequest_ShouldReturnOk(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        public static PagedList<OutputUserDto> GetAllOutputUsersDto()
        {
            var outputUsers = new List<OutputUserDto>()
            {
                new OutputUserDto()
                {
                    Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                    UserName = "ivan",
                    Email = "test@gmail.com",
                    PhoneNumber = "+375112345781"
                },
                new OutputUserDto()
                {
                    Id = "52fc493e-ed46-4df7-5544-08db734a420d",
                    UserName = "vlad",
                    Email = "vlad@gmail.com",
                    PhoneNumber = "+375295712360"
                },
                new OutputUserDto()
                {
                    Id = "c7264143-e47a-42e4-b97a-29d02088282a",
                    UserName = "kirill",
                    Email = "kirill@gmail.com",
                    PhoneNumber = "+375103912871"
                }
            };

            return PagedList<OutputUserDto>.ToPagedList(outputUsers, 3, 1, 3);
        }
    }


}
