namespace OnlineVotingSystem.api.DTOs.Vote;

public record VoteDetailsDto(
    Guid Id,
    Guid UserId,
    Guid CandidateId,
    Guid ElectionPositionId,
    DateTime Timestamp
);
