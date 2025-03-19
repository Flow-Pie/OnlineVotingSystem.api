namespace OnlineVotingSystem.api.DTOs.Election;

public record ElectionDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime StartTime,
    DateTime EndTime,
    Guid CreatedBy
);