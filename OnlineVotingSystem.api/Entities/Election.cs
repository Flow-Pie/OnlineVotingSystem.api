using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.api.Entities;

public class Election
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(255)] public required string Title { get; set; }

    [StringLength(255)] public string? Description { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    [ForeignKey("User")]public Guid CreatedBy { get; set; }
    public User? User { get; set; }
}