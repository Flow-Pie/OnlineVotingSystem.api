using WebUI.DTOs.Election;
using WebUI.DTOs.ElectionPosition;
using WebUI.DTOs.Position;

namespace WebUI.Services;

public interface IElectionsService
{
    Task<IEnumerable<ElectionDetailsDto>> GetElectionsAsync();
    Task<IEnumerable<ElectionResultsView>> GetElectionsResultsAsync();

    Task<ElectionDetailsDto> GetElectionAsync(Guid electionId);
    Task<IEnumerable<ElectionPositionSerialized>> GetElectionPositionsAsync(Guid electionId);
    Task<ElectionDetailsDto> CreateElectionAsync(CreateElectionDto createElectionDto, string token);
    Task<ElectionPositionSerialized> CreateElectionPositionAsync(Guid electionId, CreateElectionPositionDto createPositionDto);
    Task<bool> DeleteElectionAsync(Guid electionId, string token);
    Task<ElectionDetailsDto> UpdateElectionAsync(Guid electionId, string title, string? description, DateTime startTime, DateTime endTime);
}