namespace OnlineVotingSystem.api.DTOs.Candidate;

public record CandidateSerializedDto(
    Guid Id,
    string Name,
    string Position,
    string Election,
    string Bio,
    string Party,
    Uri PhotoUrl
);