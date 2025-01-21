using System.Net.Http.Json;
using Xunit.Abstractions;

namespace TensStar.IntegrationTests;

public class WebApiRequestTests
{
    private readonly ITestOutputHelper _output;

    public WebApiRequestTests(ITestOutputHelper output)
    {
        _output = output;
        new WebApiFixture();
    }

    [Fact]
    public async Task EndpointPostUsers_HappyPath_OK()
    {
        // Arrange
        using var httpClient = new HttpClient { BaseAddress = new Uri(WebApiFixture.BASE_URL) };

        var users = new List<UserDto>
        {
            new UserDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                Username = "johndoe"
            },
            new UserDto
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Password = "securePass456",
                Username = "janesmith"
            }
        };

        // Act
        var response = await httpClient.PostAsJsonAsync("api/users", users);

        // Log the response
        string responseContent = await response.Content.ReadAsStringAsync();
        _output.WriteLine("Status Code: {0}", response.StatusCode);
        _output.WriteLine("Response Content: {0}", responseContent);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    public class UserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
    }
}
