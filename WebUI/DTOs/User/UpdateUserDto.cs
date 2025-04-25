using System;

namespace WebUI.DTOs.User;

public record UpdateUserDto(
    string Name,
    string Email,
    string Password
);