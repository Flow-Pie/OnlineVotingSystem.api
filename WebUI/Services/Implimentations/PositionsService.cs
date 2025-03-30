using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.Position;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebUI.Services;

public class PositionsService : IPositionsService
{
    private readonly HttpClient httpClient;

    public PositionsService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }


    public async Task<PositionDetails> CreatePositionAsync(CreatePositionDto createPositionDto, string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty", nameof(token));

        var request = new HttpRequestMessage(HttpMethod.Post, "/positions/create")
        {
            Content = JsonContent.Create(createPositionDto),
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
        };

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode(); 

        return await response.Content.ReadFromJsonAsync<PositionDetails>()
            ?? throw new InvalidOperationException("Failed to deserialize position details.");
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