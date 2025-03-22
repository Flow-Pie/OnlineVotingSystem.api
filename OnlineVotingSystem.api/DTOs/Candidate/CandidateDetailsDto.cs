namespace OnlineVotingSystem.api.DTOs.Candidate;

public record CandidateDetailsDto(
    Guid Id,
    Guid UserId,
    Guid ElectionPositionId,
    string Bio,
    string Party,
    Uri PhotoUrl
);