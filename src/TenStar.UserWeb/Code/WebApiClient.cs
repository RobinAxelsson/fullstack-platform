using System.Text;
using System.Text.Json;

namespace TenStar.UserWeb.Code;
public class WebApiClient
{
    private const string API_URL =
#if DEBUG
        "http://localhost:5157/api";
#else
        "/api";
#endif

    public static async Task PostUserTable(HttpClient httpClient, User[] users)
    {
        if (users.Length < 1) return;

        var jsonContent = JsonSerializer.Serialize(users);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync($"{API_URL}/users", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseBody}");
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Request error: {ex.Message}");
            throw;
        }
    }
}
