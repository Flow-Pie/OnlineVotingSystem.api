using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineVotingSystem.api.DTOs.User;

namespace WebUI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private string identifier = "10000001";
    private string password = "AdminPassword!1234";

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

     public async Task<string?> GetTokenAsync()
        {
            var loginData = new
            {
                identifier,
                password
            };

            var response = await _httpClient.PostAsJsonAsync("/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                return responseData!.AccessToken;
            }

            return null; 
        }


    public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest)
    {
        var jsonContent = JsonContent.Create(loginRequest);
        var request = new HttpRequestMessage(HttpMethod.Post, "/auth/login")
        {
            Content = jsonContent
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Login failed: {response.StatusCode} - {errorMessage}");
        }

        return await response.Content.ReadFromJsonAsync<LoginResponseDto>()
               ?? throw new InvalidOperationException("Invalid login response");
    }

    public async Task<UserDetailsDto> RegisterAsync(CreateUserDto registerRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("/auth/register", registerRequest);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Registration failed: {response.StatusCode} - {errorMessage}");
        }

        return await response.Content.ReadFromJsonAsync<UserDetailsDto>() 
               ?? throw new InvalidOperationException("Invalid response received from registration.");
    }
}
