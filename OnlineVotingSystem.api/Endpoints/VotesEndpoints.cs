using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs.Vote;
using OnlineVotingSystem.api.Mapping;

namespace OnlineVotingSystem.api.Endpoints;

public static class VotesEndpoints
{
    public static RouteGroupBuilder MapVoteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("votes").WithParameterValidation();
        
        group.MapPost("/", async (CreateVoteDto voteDto, OnlineVotingSystemContext dbContext) =>
        {
            var candidate = await dbContext.Candidates
                .Include(c => c.User)
                .Include(c => c.ElectionPosition)
                    .ThenInclude(ep => ep!.Election)
                .Include(c => c.ElectionPosition)
                    .ThenInclude(ep => ep!.Position)
                .FirstOrDefaultAsync(c => c.Id == voteDto.CandidateId);

            if (candidate is null)
            {
                return Results.BadRequest("Invalid candidate.");    
            }

            var electionPositionId = candidate.ElectionPosition!.Id;

            var hasVoted = await dbContext.Votes
                .AnyAsync(v => v.UserId == voteDto.UserId && v.ElectionPositionId == electionPositionId);

            if (hasVoted)
            {
                return Results.Conflict("User has already voted for this position in this election.");
            }

            var vote = voteDto.ToEntity();

            dbContext.Votes.Add(vote);
            await dbContext.SaveChangesAsync();

            return Results.Created("/", vote.ToVoteSerializedDto());
        });


        return group;
    }
}