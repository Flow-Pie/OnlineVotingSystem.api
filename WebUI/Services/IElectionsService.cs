using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.ElectionPosition;
using OnlineVotingSystem.api.DTOs.Position;

namespace WebUI.Services;

public interface IElectionsService
{
    Task<IEnumerable<ElectionDetailsDto>> GetElectionsAsync();
    Task<ElectionDetailsDto> GetElectionAsync(Guid electionId);
    Task<IEnumerable<PositionDetails>> GetElectionPositionsAsync(Guid electionId);
    Task<ElectionDetailsDto> CreateElectionAsync(CreateElectionDto createElectionDto);
    Task<ElectionPositionSerialized> CreateElectionPositionAsync(Guid electionId, CreateElectionPositionDto createPositionDto);
    Task<bool> DeleteElectionAsync(Guid electionId);
    Task<ElectionDetailsDto> UpdateElectionAsync(Guid electionId, string title, string? description, DateTime startTime, DateTime endTime);
}