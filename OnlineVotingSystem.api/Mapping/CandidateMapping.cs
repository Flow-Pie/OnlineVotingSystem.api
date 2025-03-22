using OnlineVotingSystem.api.DTOs.Candidate;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class CandidateMapping
{
    public static CandidateDetailsDto ToCandidateDetailsDto(this Candidate candidate)
    {
        return new CandidateDetailsDto
        (
            candidate.Id,
            candidate.UserId,
            candidate.ElectionPositionId,
            candidate.Bio,
            candidate.Party!,
            candidate.PhotoUrl!
        );
    }
    
    public static CandidateSerializedDto ToCandidateSerializedDto(this Candidate candidate)
    {
        return new CandidateSerializedDto
        (
            candidate.Id,
            candidate.User!.Name,
            candidate.ElectionPosition!.Position?.Name!,
            candidate.ElectionPosition!.Election?.Title!,
            candidate.Bio,
            candidate.Party!,
            candidate.PhotoUrl!
        );
    }

    public static Candidate ToEntity(this CreateCandidateDto candidate, Guid userId, Guid electionPositionId)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ElectionPositionId = electionPositionId,
            Bio = candidate.Bio,
            Party = candidate.Party,
            PhotoUrl = candidate.PhotoUrl
        };
    }
}