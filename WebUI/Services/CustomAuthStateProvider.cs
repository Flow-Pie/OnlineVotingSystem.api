using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    // Constructor: Injects local storage service (for storing JWT) and HTTP client (if needed for API calls)
    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    // Retrieves the authentication state (called automatically by Blazor)
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get the stored JWT token from local storage
        var token = await _localStorage.GetItemAsync<string>("jwt_token");

        // If no token exists, return an empty (unauthenticated) user
        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Parse the JWT token into claims (user info)
        var claims = ParseClaimsFromJwt(token);

        // Create a ClaimsPrincipal with authentication type "jwt"
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    // Extracts claims (user details) from the JWT token
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        
        // Decode the JWT token
        var token = handler.ReadJwtToken(jwt);

        // Get all claims from the token
        var claims = token.Claims.ToList();

        // Extract the "nameid" claim (which contains both User ID and Full Name in an array)
        var nameidClaim = claims.FirstOrDefault(c => c.Type == "nameid");
        if (nameidClaim != null)
        {
            try
            {
                // Convert JSON string to a List<string> (since "nameid" contains an array)
                var nameidValues = JsonSerializer.Deserialize<List<string>>(nameidClaim.Value);
                
                if (nameidValues?.Count == 2)
                {
                    // Add User ID as a separate claim
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, nameidValues[0])); 
                    
                    // Add Full Name as a separate claim
                    claims.Add(new Claim(ClaimTypes.Name, nameidValues[1])); 
                }
            }
            catch (JsonException)
            {
                // If deserialization fails, store the raw value as User ID
                claims.Add(new Claim(ClaimTypes.NameIdentifier, nameidClaim.Value));
            }
        }

        return claims;
    }

    // Notifies Blazor that the user has logged in
    public void NotifyUserAuthentication(string token)
    {
        // Extract claims from the provided token
        var claims = ParseClaimsFromJwt(token);

        // Create a user object with the extracted claims
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        // Notify the system that authentication state has changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    // Notifies Blazor that the user has logged out
    public void NotifyUserLogout()
    {
        // Create an empty user (unauthenticated)
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

        // Notify the system that authentication state has changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}
