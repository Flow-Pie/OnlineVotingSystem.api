using System;

namespace WebUI.DTOs.User;

public record LoginResponseDto(
    UserDetailsDto User,
    string AccessToken,
    int ExpiresIn
);