using System;

using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs.User;

public record LoginUserDto(
    [Required] string Identifier,
    [Required] string Password
);