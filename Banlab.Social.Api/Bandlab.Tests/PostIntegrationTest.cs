using Microsoft.AspNetCore.Mvc.Testing;
using Banlab.Social;
using Banlab.Social.Api.Domain;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Banlab.Social.Api.ViewModels;

namespace Bandlab.Tests
{

    public class ApiTestFixture : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HttpClient CreateClient()
        {
            return _factory.CreateClient();
        }

        public ApiTestFixture()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
    public class PostIntegrationTest
     : IClassFixture<ApiTestFixture>
    {
        private readonly HttpClient _client;

        public PostIntegrationTest(ApiTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }
        [Fact]
        public async Task Post_CreatesNewPost_ReturnsSuccess()
        {
            // Arrange
            var newPost = new CreatePostViewModel { Caption = "caption", CreatorId = "1", ImageUrl = "httpsd", OriginalImageUrl = "httpgoogle.com", UserId = "1" };

            var content = JsonContent.Create(newPost);

            // Act
            var response = await _client.PostAsync("/Post", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}