using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs;

public record CreateUserDto(
    [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "Name must be more than 6 chars")]
    string Name,
    [Required]
    [Range(10000000, 99999999, ErrorMessage = "National Id must be 8 digits.")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "National ID cannot start with a zero.")]
    int NationalId,
    [Required, MaxLength(100, ErrorMessage = "Cannot exceed 100 characters")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$",
        ErrorMessage = "Email must be a valid .com address.")]
    string Email,
    [Required]
    [StringLength(120, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+]).{8,}$",
        ErrorMessage = "Password must contain at least 1 uppercase letter, 1 number, and 1 special character.")]
    string Password
);