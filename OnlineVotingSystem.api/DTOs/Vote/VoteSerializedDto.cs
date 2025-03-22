namespace OnlineVotingSystem.api.DTOs.Vote;

public record VoteSerializedDto(
    Guid Id,
    string UserName,
    string CandidateName,
    string ElectionTitle,
    string PositionName,
    DateTime Timestamp
);