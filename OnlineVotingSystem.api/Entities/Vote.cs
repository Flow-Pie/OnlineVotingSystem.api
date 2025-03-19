using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.api.Entities;

public class Vote
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("User")] public Guid UserId { get; set; }

    public required User User { get; set; }

    [ForeignKey("Election")] public Guid ElectionId { get; set; }

    public required Election Election { get; set; }

    [ForeignKey("ElectionPosition")] public Guid ElectionPositionId { get; set; }

    public required ElectionPosition ElectionPosition { get; set; }

    [ForeignKey("Candidate")] public Guid CandidateId { get; set; }

    public required Candidate Candidate { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}