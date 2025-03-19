namespace OnlineVotingSystem.api.DTOs;

public record UserDetailsDto(
    Guid Id,
    string Name,
    string Email,
    bool IsAdmin,
    DateTime CreatedAt
);