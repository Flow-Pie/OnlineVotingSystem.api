using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs.User;

public record LoginUserDto(
    [Required] string Identifier,
    [Required] string Password
);