// using System;
// using System.Collections.Generic;
// using System.Security.Claims;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Blazored.LocalStorage;
// using Microsoft.AspNetCore.Components.Authorization;

// namespace WebUI.Services
// {
//     public class CustomAuthStateProvider : AuthenticationStateProvider
//     {
//         private readonly ILocalStorageService _localStorage;

//         public CustomAuthStateProvider(ILocalStorageService localStorage)
//         {
//             _localStorage = localStorage;
//         }

//         public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//         {
//             var token = await _localStorage.GetItemAsync<string>("authToken") ?? string.Empty;
            
//             ClaimsIdentity identity = string.IsNullOrWhiteSpace(token)
//                 ? new ClaimsIdentity()
//                 : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

//             return new AuthenticationState(new ClaimsPrincipal(identity));
//         }

//         private byte[] ParseBase64WithoutPadding(string base64)
//         {
//             switch (base64.Length % 4)
//             {
//                 case 2: base64 += "=="; break;
//                 case 3: base64 += "="; break;
//             }
//             return Convert.FromBase64String(base64);
//         }

//         private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
//         {
//             var payload = jwt.Split('.')[1];
//             var jsonBytes = ParseBase64WithoutPadding(payload);
//             var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

//             var claims = new List<Claim>();

//             if (keyValuePairs.TryGetValue("IsAdmin", out var isAdmin))
//             {
//                 claims.Add(new Claim(ClaimTypes.Role, bool.Parse(isAdmin.ToString()) ? "Admin" : "User"));
//             }

//             if (keyValuePairs.TryGetValue("email", out var email))
//             {
//                 claims.Add(new Claim(ClaimTypes.Email, email.ToString()));
//             }

//             return claims;
//         }

//         public async Task NotifyUserAuthentication(string token)
//         {
//             await _localStorage.SetItemAsync("authToken", token);
//             var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
//             var user = new ClaimsPrincipal(identity);
//             NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//         }

//         public async Task NotifyUserLogout()
//         {
//             await _localStorage.RemoveItemAsync("authToken");
//             var identity = new ClaimsIdentity();
//             var user = new ClaimsPrincipal(identity);
//             NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//         }
//     }
// }


