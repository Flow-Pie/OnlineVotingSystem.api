namespace OnlineVotingSystem.api.DTOs.User;

public record UpdateUserDto(
    string Name,
    string Email,
    string Password
);