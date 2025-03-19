using OnlineVotingSystem.api.DTOs;

namespace WebUI.Services;

public class UsersService : IUsersService
{
    private readonly HttpClient httpClient;
        
    //a constructor for injecting HttpClient class to this class
    public UsersService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<UserDetailsDto>> GetUsersAsync()
    {
        return await  httpClient.GetFromJsonAsync<IEnumerable<UserDetailsDto>>("/users") 
               ??new List<UserDetailsDto>();
    }

    public async Task<UserDetailsDto?> GetUserByIdAsync(Guid id)
    {
        return await httpClient.GetFromJsonAsync<UserDetailsDto?>($"/users/{id}");

    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> NukeUsersAsync()
    {
        throw new NotImplementedException();
    }
}