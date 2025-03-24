using System.Net.Http.Headers;
using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.ElectionPosition;
using OnlineVotingSystem.api.DTOs.Position;
using OnlineVotingSystem.api.Entities;

namespace WebUI.Services;

public class ElectionService : IElectionsService
{
    private readonly HttpClient httpClient;
    
    //a constructor for injecting HttpClient class to this class
    
    public ElectionService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    } 

    public async Task<IEnumerable<ElectionResultsView>> GetElectionsResultsAsync()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<ElectionResultsView>>("/elections/results")
            ??new List<ElectionResultsView>();
    }
    

     public async Task<IEnumerable<ElectionDetailsDto>> GetElectionsAsync()
        {
        return await httpClient.GetFromJsonAsync<IEnumerable<ElectionDetailsDto>>("/elections")
            ??new List<ElectionDetailsDto>();
        }   
   

   public async Task<IEnumerable<ElectionPositionSerialized>> GetElectionPositionsAsync(Guid electionId)
    {
        var url = $"/elections/{electionId}/positions";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var positions = await response.Content.ReadFromJsonAsync<IEnumerable<ElectionPositionSerialized>>()
            ?? throw new InvalidOperationException("Failed to deserialize election positions.");

        return positions;
    }

    public async Task<ElectionDetailsDto> CreateElectionAsync(CreateElectionDto createElectionDto, string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty", nameof(token));

        var request = new HttpRequestMessage(HttpMethod.Post, "/elections/create")
        {
            Content = JsonContent.Create(createElectionDto),
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
        };

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode(); 

        return await response.Content.ReadFromJsonAsync<ElectionDetailsDto>()
            ?? throw new InvalidOperationException("Failed to deserialize election details.");
    }


    public async Task<ElectionPositionSerialized> CreateElectionPositionAsync(Guid electionId, CreateElectionPositionDto  createPositionDto)
    {
       HttpResponseMessage response = await httpClient.PostAsJsonAsync($"/elections/{electionId}/positions", createPositionDto);
       if(!response.IsSuccessStatusCode) throw new HttpRequestException($"Error creating election positions: {response.ReasonPhrase}");
       return await response.Content.ReadFromJsonAsync<ElectionPositionSerialized>()
           ?? throw new InvalidOperationException("Failed to deserialize election positions.");
    }    

    public async Task<ElectionDetailsDto> GetElectionAsync(Guid electionId)
    {
        var response = await httpClient.GetAsync($"/elections/{electionId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ElectionDetailsDto>() 
            ?? throw new InvalidOperationException("Failed to deserialize election details.");
    }

    public async Task<bool> DeleteElectionAsync(Guid electionId, string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty", nameof(token));

        var request = new HttpRequestMessage(HttpMethod.Delete, $"/elections/{electionId}")
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
        };

        var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<ElectionDetailsDto> UpdateElectionAsync(Guid electionId, string title, string? description, DateTime startTime, DateTime endTime)
    {
        var updateData = new
        {
            Title = title,
            Description = description,
            StartTime = startTime,
            EndTime = endTime
        };

        var response = await httpClient.PutAsJsonAsync($"/elections/{electionId}", updateData);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ElectionDetailsDto>() 
            ?? throw new InvalidOperationException("Failed to deserialize updated election details.");
    }
    
}