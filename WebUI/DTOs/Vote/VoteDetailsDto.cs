using System;

namespace WebUI.DTOs.Vote;

public record VoteDetailsDto(
    Guid Id,
    Guid UserId,
    Guid CandidateId,
    Guid ElectionPositionId,
    DateTime Timestamp
);
