using OnlineVotingSystem.api.DTOs.Position;
using System.Net.Http.Json;

namespace WebUI.Services;

public class PositionsService : IPositionsService
{
    private readonly HttpClient httpClient;

    public PositionsService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<PositionDetails>> GetPositionsAsync()
    {
        try
        {
            var response = await httpClient.GetAsync("/positions");

            response.EnsureSuccessStatusCode();

            var positions = await response.Content.ReadFromJsonAsync<IEnumerable<PositionDetails>>();

            return positions ?? new List<PositionDetails>();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException($"Failed to fetch positions: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred: {ex.Message}", ex);
        }
    }
}