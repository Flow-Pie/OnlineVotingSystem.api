using System;

namespace WebUI.DTOs.Vote;

public record CreateVoteDto(
    Guid UserId,
    Guid CandidateId,
    Guid ElectionPositionId
);