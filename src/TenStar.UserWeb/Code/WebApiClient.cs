using System.Net.Http.Json;
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
        if (users.Length == 0) return;

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{API_URL}/users", users);
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
