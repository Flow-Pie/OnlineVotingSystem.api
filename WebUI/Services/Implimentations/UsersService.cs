using WebUI.DTOs.User;

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
        var response = await httpClient.DeleteAsync($"/users/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> NukeUsersAsync()
    {
        var response = await httpClient.DeleteAsync("/users/nuke");

        return response.IsSuccessStatusCode;
    }

}