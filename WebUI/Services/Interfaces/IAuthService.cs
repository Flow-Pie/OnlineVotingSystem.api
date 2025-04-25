using System.Threading.Tasks;
using WebUI.DTOs.User;

namespace WebUI.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest);    
    Task<UserDetailsDto> RegisterAsync(CreateUserDto registerRequest);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
    Task<string?> GetUserIdAsync();
    Task<string?> GetUserNameAsync();
    Task<string?> GetUserRoleAsync();
    bool IsAuthenticated();
}
