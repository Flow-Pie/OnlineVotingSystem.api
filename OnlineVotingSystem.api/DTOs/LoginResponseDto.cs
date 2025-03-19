namespace OnlineVotingSystem.api.DTOs;

public record LoginResponseDto(
    UserDetailsDto User,
    string AccessToken,
    int ExpiresIn
);