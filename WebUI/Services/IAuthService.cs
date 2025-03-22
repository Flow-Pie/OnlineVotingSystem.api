using System.Threading.Tasks;
using OnlineVotingSystem.api.DTOs.User;

namespace WebUI.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginUserDto loginRequest);
    Task<UserDetailsDto> RegisterAsync(CreateUserDto registerRequest);
}
