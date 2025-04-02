using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Microsoft.Extensions.Caching.Memory;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly IMemoryCache _cache;
    private const string TokenCacheKey = "jwt_token"; // Cache key for the token
    private const double TokenExpiryInMinutes = 30; // Cache expiry time

    public CustomAuthStateProvider(ILocalStorageService localStorage, IMemoryCache cache)
    {
        _localStorage = localStorage;
        _cache = cache;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // First, check if the token is available in cache
        if (_cache.TryGetValue(TokenCacheKey, out string cachedToken))
        {
            // Token found in cache, proceed to create AuthenticationState
            return new AuthenticationState(CreateClaimsPrincipal(cachedToken));
        }

        // Token not found in cache, check local storage
        string token = await _localStorage.GetItemAsync<string>(TokenCacheKey);

        if (!string.IsNullOrEmpty(token))
        {
            // Token found in local storage, cache it for future use
            _cache.Set(TokenCacheKey, token, TimeSpan.FromMinutes(TokenExpiryInMinutes));
            return new AuthenticationState(CreateClaimsPrincipal(token));
        }

        // No token found, return anonymous user
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    private ClaimsPrincipal CreateClaimsPrincipal(string token)
    {
        // Parse the claims from the JWT token and return the ClaimsPrincipal
        var claims = ParseClaimsFromJwt(token);
        return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var claims = token.Claims.ToList();

        // Extract "nameid" values
        var nameidClaims = claims.Where(c => c.Type == "nameid").Select(c => c.Value).ToList();

        if (nameidClaims.Count >= 1)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameidClaims[0])); // User ID
        }
        if (nameidClaims.Count == 2 && !claims.Any(c => c.Type == ClaimTypes.Name))
        {
            claims.Add(new Claim(ClaimTypes.Name, nameidClaims[1])); // Full Name
        }

        // Add role if user is admin
        if (claims.Any(c => c.Type == "isAdmin" && c.Value.Equals("true", StringComparison.OrdinalIgnoreCase)))
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }else{
            claims.Add(new Claim(ClaimTypes.Role, "User"));
        }

        return claims;
    }

    public async Task NotifyUserAuthentication(string token)
    {
        // Store the token in both cache & local storage
        _cache.Set(TokenCacheKey, token, TimeSpan.FromMinutes(TokenExpiryInMinutes));
        await _localStorage.SetItemAsync(TokenCacheKey, token);

        // Notify UI of login state with the user claims
        var user = CreateClaimsPrincipal(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task NotifyUserLogout()
    {
        // Clear the token from cache and local storage
        _cache.Remove(TokenCacheKey);
        await _localStorage.RemoveItemAsync(TokenCacheKey);

        // Notify UI of logout state with an anonymous user
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}
