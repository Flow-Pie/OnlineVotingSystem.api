using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs.Candidate;

public record UpdateCandidateDto(
    Guid ElectionPositionId,
    [StringLength(100)] string? Party,
    [StringLength(255)] string Bio,
    [StringLength(255)] Uri? PhotoUrl
);