using System;

namespace WebUI.DTOs.User;

public record UserDetailsDto(
    Guid Id,
    string Name,
    string Email,
    int NationalId,
    bool IsAdmin,
    DateTime CreatedAt
);