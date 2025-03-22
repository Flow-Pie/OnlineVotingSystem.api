namespace OnlineVotingSystem.api.DTOs.Vote;

public record CreateVoteDto(
    Guid UserId,
    Guid CandidateId,
    Guid ElectionPositionId
);