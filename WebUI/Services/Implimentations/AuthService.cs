using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineVotingSystem.api.DTOs.User;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Caching.Memory;

namespace WebUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navManager;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "auth_token_cache"; 

        public AuthService(HttpClient httpClient, 
                           ILocalStorageService localStorage, 
                           AuthenticationStateProvider authenticationStateProvider, 
                           NavigationManager navManager,  
                           IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _navManager = navManager; 
            _memoryCache = memoryCache;
        }

        public async Task<string?> GetTokenAsync()
        {
            // 1. First try memory cache (works during prerendering)
            if (_memoryCache.TryGetValue(CacheKey, out string? cachedToken))
            {
                return cachedToken;
            }

            // 2. Fallback to localStorage (browser-only)
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                if (!string.IsNullOrEmpty(token))
                {
                    // Cache in memory for 30 minutes
                    _memoryCache.Set(CacheKey, token, TimeSpan.FromMinutes(30));  // Use 'token' here
                    return token;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LocalStorage fallback failed: {ex.Message}");
            }

            return null;
        }

        public void SetAuthHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }

        private bool IsPrerendering()
        {
            // For Blazor WebAssembly, prerendering ends quickly
            return !OperatingSystem.IsBrowser();
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(1);
            }
            catch (Exception)
            {
                // Token might be invalid or corrupted, so consider it expired
                return true;
            }
        }

        // Login method that retrieves credentials from local storage
        public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/auth/login", loginRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Login failed: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (result == null || string.IsNullOrEmpty(result.AccessToken))
                {
                    throw new InvalidDataException("Invalid server response or missing access token.");
                }

                SetAuthHeader(result.AccessToken);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                // Store token in localStorage and cache
                await _localStorage.SetItemAsync("authToken", result.AccessToken);
                _memoryCache.Set(CacheKey, result.AccessToken, cacheOptions);

                // Notify authentication state provider
                ((CustomAuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(result.AccessToken);

                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                throw new Exception($"Login failed. Check your network or credentials: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw new Exception($"Login failed due to an unexpected error: {ex.Message}", ex);
            }
        }

        // Registration using credentials from local storage
        public async Task<UserDetailsDto> RegisterAsync(CreateUserDto registerRequest)
        {
            try
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
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Registration error: {ex.Message}");
                throw new Exception($"Registration failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw new Exception($"Registration failed due to an unexpected error: {ex.Message}", ex);
            }
        }

        // Logout method that removes the token from local storage and notifies authentication state
        public async Task LogoutAsync()
        {
            try
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("rememberToken");

                _memoryCache.Remove(CacheKey);

                // Notify logout to authentication state provider
                ((CustomAuthStateProvider)_authenticationStateProvider).NotifyUserLogout();

                // Navigate to login page
                _navManager.NavigateTo("/login", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout failed: {ex.Message}");
                throw new Exception($"Logout failed due to an error: {ex.Message}", ex);
            }
        }
    }
}