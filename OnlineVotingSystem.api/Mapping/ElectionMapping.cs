using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class ElectionMapping
{
    public static ElectionDetailsDto ToElectionDetailsDto(this Election election)
    {
        return new ElectionDetailsDto
        (
            election.Id,
            election.Title,
            election.Description,
            election.StartTime,
            election.EndTime,
            election.CreatedBy
        );
    }

     public static ElectionResultsView ToElectionResultsDetailsDto(this ElectionResult election)
    {
        return new ElectionResultsView        
        (
            election.ElectionId ?? Guid.Empty,                // If null, use Guid.Empty
            election.ElectionTitle,
            election.ElectionPositionId ?? Guid.Empty,
            election.PositionId ?? Guid.Empty,
            election.PositionName,
            election.CandidateId ?? Guid.Empty,
            election.CandidateUserId ?? Guid.Empty,
            election.CandidateName,
            election.Party,
            election.TotalVotes,
            election.RegisteredVoters,
            election.VoterNames
        );
    }


    public static Election ToEntity(this CreateElectionDto election, Guid userId)
    {
        return new Election
        {
            Id = Guid.NewGuid(),
            Title = election.Title,
            Description = election.Description,
            StartTime = election.StartTime,
            EndTime = election.EndTime,
            CreatedBy = userId
        };
    }

    public static void ApplyUpdates(this Election election, UpdateElectionDto updateElection)
    {
        if (!string.IsNullOrWhiteSpace(updateElection.Title)) election.Title = updateElection.Title!;
        if (!string.IsNullOrWhiteSpace(updateElection.Description)) election.Description = updateElection.Description!;
        if (updateElection.StartTime.HasValue) election.StartTime = updateElection.StartTime.Value;
        if (updateElection.EndTime.HasValue) election.EndTime = updateElection.EndTime.Value;
    }
}
