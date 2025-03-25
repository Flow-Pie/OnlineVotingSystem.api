using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineVotingSystem.api.Entities;
public class ElectionResult
{
    public Guid? CandidateId { get; set; }
    public Guid? CandidateUserId { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public string Party { get; set; } = string.Empty;
    public Guid? ElectionPositionId { get; set; }
    public Guid? PositionId { get; set; }
    public string PositionName { get; set; } = string.Empty;
    public Guid? ElectionId { get; set; }
    public string ElectionTitle { get; set; } = string.Empty;
    public int TotalVotes { get; set; }
    public int RegisteredVoters { get; set; }
    public string VoterNames { get; set; } = string.Empty;
    
}
