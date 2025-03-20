using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineVotingSystem.api.DTOs;

namespace WebUI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("/auth/login", loginRequest);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Login failed: {response.StatusCode} - {errorMessage}");
        }

        return await response.Content.ReadFromJsonAsync<LoginResponseDto>() 
               ?? throw new InvalidOperationException("Invalid response received from login.");
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
