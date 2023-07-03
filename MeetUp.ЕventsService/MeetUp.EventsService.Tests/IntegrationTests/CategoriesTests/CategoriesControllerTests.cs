using MeetUp.EventsService.Tests.IntegrationTests.CategoriesTests;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace MeetUp.EventsService.Tests.IntegrationTests.CategoriesController
{
    public class CategoriesControllerTests
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/category", StatusCodes.Status200OK)]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b1", StatusCodes.Status200OK)]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b2", StatusCodes.Status422UnprocessableEntity)]
        public async Task GetCategoriesAsync_SendRequest_ShouldReturnOk(
            string url,
            int statusCode)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData("", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("First", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("uwu", StatusCodes.Status201Created)]
        public async Task CreateCategoryAsync_SendRequest_ShouldReturnCorrectStatusCode(
            string name,
            int statusCode)
        {
            // Arrange
            string url = "/category";
            var resultContentType = "application/json; charset=utf-8";

            var formData = new Dictionary<string, string>
            {
                { "Name", name }
            };

            var jsonData = JsonConvert.SerializeObject(formData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b1", StatusCodes.Status204NoContent)]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b2", StatusCodes.Status422UnprocessableEntity)]
        public async Task DeleteCategoryAsync_SendRequest_ShouldReturnCorrectStatusCode(
            string url,
            int statusCode)
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }

        [Theory]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b1", "test", StatusCodes.Status200OK)]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b2", "test", StatusCodes.Status422UnprocessableEntity)]
        [InlineData("/category/2789b1c3-34e5-4f4a-7bbf-08db683498b1", "Second", StatusCodes.Status422UnprocessableEntity)]
        public async Task UpdateCategoryAsync_SendRequest_ShouldReturnCorrectStatusCode(
            string url,
            string name,
            int statusCode)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            var formData = new Dictionary<string, string>
            {
                { "Name", name }
            };

            var jsonData = JsonConvert.SerializeObject(formData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }
    }
}
