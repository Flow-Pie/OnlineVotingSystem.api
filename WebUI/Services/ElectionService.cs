using OnlineVotingSystem.api.DTOs.Election;
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
    
    
    public async Task<IEnumerable<ElectionDetailsDto>> GetElectionsAsync()
    {
       return await httpClient.GetFromJsonAsync<IEnumerable<ElectionDetailsDto>>("/elections")
           ??new List<ElectionDetailsDto>();
    }

    public async Task<ElectionDetailsDto> GetElectionAsync(int electionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PositionDetails>> GetElectionPositionsAsync(int electionId)
    {
        throw new NotImplementedException();
    }

    public async Task<ElectionDetailsDto> CreateElectionAsync(string title, string? description, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }

    public async Task<PositionDetails> CreateElectionPositionAsync(int electionId, int positionId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteElectionAsync(int electionId)
    {
        throw new NotImplementedException();
    }

    public async Task<ElectionDetailsDto> UpdateElectionAsync(int electionId, string title, string? description, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }
    
}