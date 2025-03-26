using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.Entities;

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public required int NationalId { get; set; }

    [StringLength(100)] public required string Name { get; set; } 

    // First Name And Second
// Phone Number
// Gender
    [StringLength(100)] public required string Email { get; set; }

    [StringLength(255)] public required string Password { get; set; }

    [DefaultValue(false)] public bool IsAdmin { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}