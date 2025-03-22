using OnlineVotingSystem.api.DTOs.Vote;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class VoteMapping
{
    public static VoteDetailsDto ToVoteDetailsDto(this Vote vote)
    {
        return new VoteDetailsDto(
            vote.Id,
            vote.UserId,
            vote.CandidateId,
            vote.ElectionPositionId,
            vote.Timestamp
        );
    }

    public static VoteSerializedDto ToVoteSerializedDto(this Vote vote)
    {
        return new VoteSerializedDto(
            vote.Id,
            vote.User!.Name,
            vote.Candidate!.User!.Name,
            vote.ElectionPosition!.Election!.Title,
            vote.ElectionPosition!.Position!.Name,
            vote.Timestamp
        );
    }

    public static Vote ToEntity(this CreateVoteDto voteDto)
    {
        return new Vote
        {
            Id = Guid.NewGuid(),
            UserId = voteDto.UserId,
            CandidateId = voteDto.CandidateId,
            ElectionPositionId = voteDto.ElectionPositionId,
            Timestamp = DateTime.UtcNow
        };
    }
}
