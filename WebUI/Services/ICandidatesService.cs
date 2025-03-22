using OnlineVotingSystem.api.DTOs.Candidate;

public interface ICandidatesService
{
    Task<IEnumerable<CandidateSerializedDto>> GetSerializedCandidatesAsync();
    
    Task<CandidateDetailsDto> GetCandidateDetailsAsync(Guid candidateId);
    
    Task<CandidateDetailsDto> CreateCandidateAsync(CreateCandidateDto candidateDto);
    
    Task<CandidateDetailsDto> UpdateCandidateAsync(Guid candidateId, UpdateCandidateDto updateDto);
    
    Task DeleteCandidateAsync(Guid candidateId);
    
    Task<IEnumerable<CandidateSerializedDto>> GetCandidatesByPositionAsync(Guid electionPositionId);
    
    Task<IEnumerable<CandidateDetailsDto>> GetCandidatesByUserAsync(Guid userId);
}