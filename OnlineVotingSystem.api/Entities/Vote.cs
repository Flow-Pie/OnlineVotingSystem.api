using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.api.Entities;

public class Vote
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("User")] public Guid UserId { get; set; }

    public User? User { get; set; }

    [ForeignKey("ElectionPosition")] public Guid ElectionPositionId { get; set; }

    public ElectionPosition? ElectionPosition { get; set; }

    [ForeignKey("Candidate")] public Guid CandidateId { get; set; }

    public Candidate? Candidate { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}