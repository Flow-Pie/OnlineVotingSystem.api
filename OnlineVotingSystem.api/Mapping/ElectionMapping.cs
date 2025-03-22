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
