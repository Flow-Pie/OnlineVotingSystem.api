using OnlineVotingSystem.api.DTOs;

namespace WebUI.Services;

public interface IUsersService
{
    Task<IEnumerable<UserDetailsDto>> GetUsersAsync();  
    Task <UserDetailsDto?> GetUserByIdAsync(Guid id);   
    Task<bool> DeleteUserAsync(Guid id);         
    Task<bool> NukeUsersAsync();                 // DELETE /users/nuke
}
