using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using OnlineVotingSystem.api.DTOs.User;

namespace WebUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authProvider;
        private readonly NavigationManager _navManager;
        private readonly IMemoryCache _memoryCache;

        private const string TokenCacheKey = "auth_token_cache";
        private const string UserIdCacheKey = "auth_userid_cache";
        private const string UserNameCacheKey = "auth_username_cache";
        private const string TokenStorageKey = "authToken";
        private const string RememberTokenKey = "rememberToken";
        private const string UserRoleCacheKey = "user_role_cache";

        //pass all required services to the constructor so that they can be injected automatically by asp whenever we call authservice
        public AuthService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authProvider,
            NavigationManager navManager,
            IMemoryCache memoryCache)//CRITICAL SERVICE-WITHOUT THIS THE APP CRASHES WHEN RETRIEVAL OF TOKEN FROM LOCAL STORAGE FAILS ie jsInterop error
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authProvider = authProvider;
            _navManager = navManager;
            _memoryCache = memoryCache;
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                // 1. Check memory cache first
                if (_memoryCache.TryGetValue(TokenCacheKey, out string? cachedToken))
                {
                    Console.WriteLine("[AuthService] Retrieved token from memory cache");
                    return cachedToken;
                }
                Console.WriteLine("[AuthService] failed to retrieve token from memory cache trying local storage");

                // 2. Fallback to localStorage
                try
                {
                    var token = await _localStorage.GetItemAsync<string>(TokenStorageKey);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var cacheOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(30));
                        
                        _memoryCache.Set(TokenCacheKey, token, cacheOptions);
                        Console.WriteLine("[AuthService] Retrieved and cached token from local storage");
                        return token;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AuthService] ERROR - LocalStorage fallback failed: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }

                // 3. No token found - navigate to login
                Console.WriteLine("[AuthService] WARNING - No valid token found, redirecting to login");
                _navManager.NavigateTo("/login", forceLoad: true);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] CRITICAL ERROR - GetTokenAsync failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                _navManager.NavigateTo("/login", forceLoad: true);
                return null;
            }
        }
        //get authenticated user id
        public async Task<string?> GetUserIdAsync()
        {
            try
            {
                if (_memoryCache.TryGetValue(UserIdCacheKey, out string? cachedUserId))
                {
                    Console.WriteLine("[AuthService] Retrieved user ID from memory cache");
                    return cachedUserId;
                }

                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("[AuthService] WARNING - Cannot get user ID (no token available)");
                    return null;
                }

                // Retrieve all claims with type "nameid" and select the first element (index 0)
                var userId = ParseClaimFromToken(token, "nameid", isArray: true, index: 0);
                if (!string.IsNullOrEmpty(userId))
                {
                    _memoryCache.Set(UserIdCacheKey, userId, TimeSpan.FromMinutes(30));
                    Console.WriteLine($"[AuthService] Parsed and cached user ID: {userId}");
                }
                else
                {
                    Console.WriteLine("[AuthService] WARNING - User ID claim not found in token");
                }

                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] ERROR - Failed to get user ID: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<string?> GetUserNameAsync()
        {
            try
            {
                if (_memoryCache.TryGetValue(UserNameCacheKey, out string? cachedUserName))
                {
                    Console.WriteLine("[AuthService] Retrieved username from memory cache");
                    return cachedUserName;
                }

                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("[AuthService] WARNING - Cannot get username (no token available)");
                    return null;
                }

                // Retrieve all "nameid" claims and select the second element (index 1) for the full name
                var userName = ParseClaimFromToken(token, "nameid", isArray: true, index: 1);
                if (!string.IsNullOrEmpty(userName))
                {
                    _memoryCache.Set(UserNameCacheKey, userName, TimeSpan.FromMinutes(30));
                    Console.WriteLine($"[AuthService] Parsed and cached username: {userName}");
                }
                else
                {
                    Console.WriteLine("[AuthService] WARNING - Username not found in token");
                }

                return userName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] ERROR - Failed to get username: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public async Task<string?> GetUserRoleAsync()
            {
                try
                {
                    // 1. Check memory cache for user role
                    if (_memoryCache.TryGetValue(UserRoleCacheKey, out string? cachedRole))
                    {
                        Console.WriteLine("[AuthService] Retrieved user role from memory cache");
                        return cachedRole;
                    }

                    // 2. Fallback to the token to get the role
                    var token = await GetTokenAsync();
                    if (string.IsNullOrEmpty(token))
                    {
                        Console.WriteLine("[AuthService] WARNING - Cannot get user role (no token available)");
                        return null;
                    }

                    // Check if isAdmin claim exists and is true
                    var isAdmin = ParseClaimFromToken(token, "isAdmin", isArray: false, index: 0);
                    if (!string.IsNullOrEmpty(isAdmin) && bool.TryParse(isAdmin, out bool isAdminValue) && isAdminValue)
                    {
                        var role = "Admin";
                        _memoryCache.Set(UserRoleCacheKey, role, TimeSpan.FromMinutes(30));
                        Console.WriteLine($"[AuthService] Parsed and cached user role: {role}");
                        return role;
                    }
                    else
                    {
                        var role = "User";
                        _memoryCache.Set(UserRoleCacheKey, role, TimeSpan.FromMinutes(30));
                        Console.WriteLine($"[AuthService] Parsed and cached user role: {role}");
                        return role;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AuthService] ERROR - Failed to get user role: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }




        /// <summary>
        /// jwt json example "nameid": ["af3bf570-3ab0-40df-ba5d-5798b682491c","Jane Wangari" ]
        /// Parses a claim from a JWT token. If isArray is true, it gathers all claims with the specified type
        /// and returns the element at the provided index.
        /// </summary>
        
        /// <returns>The value of the claim, or null if not found.</returns>
        private string? ParseClaimFromToken(string token, string claimType, bool isArray, int index)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                
                // Log the claims to inspect their structure
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($"Claim type: {claim.Type}, value: {claim.Value}");
                }

                var claims = jwtToken.Claims.Where(c => c.Type == claimType).Select(c => c.Value).ToArray();
                
                if (claims == null || claims.Length == 0)
                {
                    Console.WriteLine($"[AuthService] WARNING - Claim {claimType} not found in token");
                    return null;
                }

                if (isArray)
                {
                    Console.WriteLine($"[AuthService] Retrieved array claim {claimType}: {string.Join(" | ", claims)}");
                    if (claims.Length > index)
                    {
                        return claims[index];
                    }
                    else
                    {
                        Console.WriteLine($"[AuthService] WARNING - Claim {claimType} does not have enough elements; found {claims.Length} elements, needed index {index}");
                        return null;
                    }
                }
                else
                {
                    return claims[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] ERROR - Failed to parse {claimType}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public bool IsAuthenticated()
        {
            try
            {
                // Check memory cache first
                if (!_memoryCache.TryGetValue(TokenCacheKey, out string token) || 
                    string.IsNullOrEmpty(token))
                {
                    return false;
                }

                // Validate token structure without JS dependencies
                if (!IsValidTokenFormat(token))
                {
                    _memoryCache.Remove(TokenCacheKey);
                    return false;
                }

                // Check token expiration
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token)) return false;
                
                var jwtToken = handler.ReadJwtToken(token);
                return jwtToken.ValidTo > DateTime.UtcNow.AddMinutes(1);
            }
            catch
            {
                // Fail safe - treat as not authenticated
                return false;
            }
        }

        // Basic JWT structure validation without JS dependencies
        private bool IsValidTokenFormat(string token)
        {
            // Basic JWT structure validation
            var parts = token.Split('.');
            return parts.Length == 3 && 
                parts[0].Length > 10 && 
                parts[1].Length > 10 && 
                parts[2].Length > 10;
        }


//set auth header to each htttp request
        public void SetAuthHeader(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("[AuthService] WARNING - Attempted to set empty auth header");
                    return;
                }

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("[AuthService] Authorization header set successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] ERROR - Failed to set auth header: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
//login method
        public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest)
        {
            try
            {
                Console.WriteLine($"[AuthService] Attempting login for Identifier: {loginRequest.Identifier}");
                var response = await _httpClient.PostAsJsonAsync("/auth/login", loginRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[AuthService] ERROR - Login failed (Status: {response.StatusCode}): {errorContent}");
                    throw new HttpRequestException($"Login failed: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (result == null || string.IsNullOrEmpty(result.AccessToken))
                {
                    Console.WriteLine("[AuthService] ERROR - Invalid login response from server");
                    throw new InvalidDataException("Invalid server response");
                }

                SetAuthHeader(result.AccessToken);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                try
                {
                    await _localStorage.SetItemAsync(TokenStorageKey, result.AccessToken);
                    _memoryCache.Set(TokenCacheKey, result.AccessToken, cacheOptions);
                    _memoryCache.Remove(UserIdCacheKey);
                    _memoryCache.Remove(UserNameCacheKey);
                    _memoryCache.Remove(UserRoleCacheKey); // Remove 
                    ((CustomAuthStateProvider)_authProvider).NotifyUserAuthentication(result.AccessToken);
                    Console.WriteLine($"[AuthService] Login successful for email: {loginRequest.Identifier}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Login error: {ex.Message}");
                    throw new Exception($"Login failed. Check your network or credentials: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AuthService] ERROR - Failed to store authentication tokens: {ex.Message}");                   
                    throw new Exception($"Login failed due to an unexpected error: {ex.Message}", ex);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] CRITICAL ERROR - Login process failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw new Exception($"Login failed due to an unexpected error, Either wrong credentials: {ex.Message}", ex);
            }
        }

//CRITICAL - MEMORY CACHE MUST PROPERLY BE INVALIDATED
        public async Task LogoutAsync()
        {
            try
            {
                Console.WriteLine("[AuthService] Initiating logout process");
                
                await _localStorage.RemoveItemAsync(TokenStorageKey);
                await _localStorage.RemoveItemAsync(RememberTokenKey);
                await _localStorage.RemoveItemAsync(UserRoleCacheKey); 

                _memoryCache.Remove(TokenCacheKey);
                _memoryCache.Remove(UserIdCacheKey);
                _memoryCache.Remove(UserNameCacheKey);
                _memoryCache.Remove(UserRoleCacheKey); 

                _httpClient.DefaultRequestHeaders.Authorization = null;
                
                ((CustomAuthStateProvider)_authProvider).NotifyUserLogout();
                _navManager.NavigateTo("/login", true);
                
                Console.WriteLine("[AuthService] Logout completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] CRITICAL ERROR - Logout failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public async Task<UserDetailsDto> RegisterAsync(CreateUserDto registerRequest)
        {
            try
            {
                Console.WriteLine($"[AuthService] Attempting registration for email: {registerRequest.Email}");
                var response = await _httpClient.PostAsJsonAsync("/auth/register", registerRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[AuthService] ERROR - Registration failed (Status: {response.StatusCode}): {errorContent}");
                    throw new HttpRequestException($"Registration failed: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<UserDetailsDto>();
                if (result == null)
                {
                    Console.WriteLine("[AuthService] ERROR - Invalid registration response from server");
                    throw new InvalidOperationException("Invalid response received from registration");
                }

                Console.WriteLine($"[AuthService] Registration successful for email: {registerRequest.Email}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] CRITICAL ERROR - Registration failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}