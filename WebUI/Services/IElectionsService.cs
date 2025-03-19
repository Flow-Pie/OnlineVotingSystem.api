using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.Position;

namespace WebUI.Services;

public interface IElectionsService
{
    Task<IEnumerable<ElectionDetailsDto>> GetElectionsAsync();
    Task<ElectionDetailsDto> GetElectionAsync(int electionId);
    Task<IEnumerable<PositionDetails>> GetElectionPositionsAsync(int electionId);
    Task<ElectionDetailsDto> CreateElectionAsync(string title, string? description, DateTime startTime, DateTime endTime);
    Task<PositionDetails> CreateElectionPositionAsync(int electionId, int positionId);
    Task<bool> DeleteElectionAsync(int electionId);
    Task<ElectionDetailsDto> UpdateElectionAsync(int electionId, string title, string? description, DateTime startTime, DateTime endTime);
}