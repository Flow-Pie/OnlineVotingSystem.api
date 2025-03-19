using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs;

public record LoginUserDto(
    [Required] string Identifier,
    [Required] string Password
);