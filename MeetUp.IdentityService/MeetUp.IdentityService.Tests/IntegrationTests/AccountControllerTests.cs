using Elasticsearch.Net;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace MeetUp.IdentityService.Tests.IntegrationTests
{
    public class AccountControllerTests
    {
        private readonly HttpClient _client;

        public AccountControllerTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/account")]
        [InlineData("/account/test@gmail.com")]
        public async Task GetUsersAsync_SendRequest_ShouldReturnOk(string url)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData("/account/uwu")]
        [InlineData("/account/a1322@gmail.com")]
        public async Task GetUsersAsync_SendRequestWithIncorrectData_ShouldReturnBadRequest(string url)
        {
            // Arrange
            var resultStatusCode = StatusCodes.Status500InternalServerError;
            var resultContentType = "application/json; charset=utf-8";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(resultStatusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("test@gmail.com", "aaa111aaa", StatusCodes.Status200OK)]
        [InlineData("igor@gmail.com", "aaaB1Bawd", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("test@gmail.com", "aaa123aaa", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("uwu", "aaa111aaa", StatusCodes.Status422UnprocessableEntity)]
        public async Task SignInAsync_SendRequest_ShouldReturnCorrectStatusCode(
            string email,
            string password,
            int statusCode)
        {
            // Arrange
            string url = "/account/SignIn";

            var formData = new Dictionary<string, string>
            {
                { "Email", email },
                { "Password", password }
            };

            var jsonData = JsonConvert.SerializeObject(formData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }

        [Theory]
        [InlineData("masha", "masha@gmail.com", "+375182957281", "aaa111aaa", StatusCodes.Status201Created)]
        [InlineData("masha", "uwu", "+375281628952", "aaa111aaa", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("ivan", "test@gmail.com", "+372017502759", "aaa111aaa", StatusCodes.Status422UnprocessableEntity)]
        public async Task SignUpAsync_SendRequest_ShouldReturnCorrectStatusCode(
            string userName,
            string email,
            string phoneNumber,
            string password,
            int statusCode)
        {
            // Arrange
            string url = "/account/SignUp";

            var formData = new Dictionary<string, string>
            {
                { "UserName", userName },
                { "Email", email },
                { "PhoneNumber", phoneNumber },
                { "Password", password }
            };

            var jsonData = JsonConvert.SerializeObject(formData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }
    }
}
