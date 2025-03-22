namespace OnlineVotingSystem.api.DTOs.User;

public record LoginResponseDto(
    UserDetailsDto User,
    string AccessToken,
    int ExpiresIn
);