using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.api.Entities;

public class Candidate
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("ElectionPosition")] public Guid ElectionPositionId { get; set; }

    public ElectionPosition? ElectionPosition { get; set; }

    [ForeignKey("User")] public Guid UserId { get; set; }
    public User? User { get; set; }
    
    [DefaultValue("independent"), StringLength(100)]
    public string? Party { get; set; }

    [StringLength(255)] public required string Bio { get; set; }

    [StringLength(255)] public Uri? PhotoUrl { get; set; }
}