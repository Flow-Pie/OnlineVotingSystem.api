using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs.Candidate;
using OnlineVotingSystem.api.Mapping;
using OnlineVotingSystem.api.Entities;
using OnlineVotingSystem.api.DTOs.Election;


namespace OnlineVotingSystem.api.Endpoints;

public static class CandidateEndpoints
{
    public static RouteGroupBuilder MapCandidateEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("candidates").WithParameterValidation();

        // GET Candidates
        group.MapGet("/", async (OnlineVotingSystemContext dbContext) =>
        {
            return await dbContext.Candidates
                .Include(candidate => candidate.User)
                .Include(candidate => candidate.ElectionPosition)
                    .ThenInclude(ep => ep!.Position) // Ensure Position is loaded
                .Include(candidate => candidate.ElectionPosition)
                    .ThenInclude(ep => ep!.Election) // Ensure Election is loaded
                .Select(candidate => candidate.ToCandidateSerializedDto())
                .AsNoTracking()
                .ToListAsync();
        }).WithName("getCandidates");
        
        // GET by ID
        group.MapGet("/{candidateId:guid}", async (Guid candidateId, OnlineVotingSystemContext dbContext) =>
        {
            var candidate = await dbContext.Candidates
                .Include(c => c.ElectionPosition)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            return candidate is not null
                ? Results.Ok(candidate.ToCandidateDetailsDto())
                : Results.NotFound();
        }).WithName("getCandidateById");


        // Create Candidate Admin only
        group.MapPost("/", async (CreateCandidateDto newCandidate, OnlineVotingSystemContext dbContext) =>
        {
            //find user by national id
            var user = await dbContext.Users.FirstOrDefaultAsync(u=>u.NationalId.ToString().Equals(newCandidate.NationalId));
            if(user==null) return Results.BadRequest("Invalid (national Id user not found");
            if(user.IsAdmin) return Results.BadRequest("Admins cannot be candidates");//FIX : ADD ADMIN CHECK            
            

            var electionPosition = await dbContext.ElectionPositions//FIX : USE POSITION ID
                .Include(_ => _.Election) // Include the Election to access its ID
                .FirstOrDefaultAsync(_ => _.PositionId == newCandidate.PositionId);
            if (electionPosition == null)
            {
                return Results.BadRequest("Invalid Position ID.");
            }            

            var electionId = electionPosition.Election!.Id;
            var userId = user.Id;

            // Check if the user is already a candidate in the same election
            var alreadyCandidate = await dbContext.Candidates
                .AnyAsync(c => c.UserId == userId && c.ElectionPosition!.ElectionId == electionId);

            if (alreadyCandidate)
            {
                return Results.Conflict("This user is already a candidate in this election.");
            }
           


            // Convert DTO to Entity
            var candidate = newCandidate.ToEntity(user.Id, electionPosition.Id);

            // Save candidate to DB
            dbContext.Candidates.Add(candidate);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("getElections", new { id = candidate.Id },
                candidate.ToCandidateSerializedDto()); 
        }).RequireAuthorization("AdminOnly");

        // Edit candidate
        group.MapPatch("/{candidateId:guid}",
        async (Guid candidateId,
            UpdateCandidateDto updateDto,
            OnlineVotingSystemContext dbContext) =>
        {
            // Find the candidate
            var candidate = await dbContext.Candidates
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null)
            {
                return Results.NotFound("Candidate not found.");
            }

            // Update candidate details
            if (!string.IsNullOrEmpty(updateDto.Bio)) candidate.Bio = updateDto.Bio;
            
            ElectionPosition? electionPosition = null;

            if (updateDto.PositionId.HasValue)
            {
                electionPosition = await dbContext.ElectionPositions
                    .FirstOrDefaultAsync(ep => ep.PositionId == updateDto.PositionId.Value);

                if (electionPosition ==null) return Results.NotFound("Invalid PositionId. No matching ElectionPosition found.");
                
                
                candidate.ElectionPositionId = electionPosition.Id; // FIXED POSITION NOT UPDATING BUG!!
            }

            if (!string.IsNullOrEmpty(updateDto.Party)) candidate.Party = updateDto.Party;
            if (updateDto.PhotoUrl is not null) candidate.PhotoUrl = updateDto.PhotoUrl;

            // Save changes
            await dbContext.SaveChangesAsync();

            return Results.Ok(candidate.ToCandidateDetailsDto());
        }).RequireAuthorization("AdminOnly");



        // Delete candidate
        group.MapDelete("/{candidateId:guid}", async (Guid candidateId, OnlineVotingSystemContext dbContext) =>
        {
            var candidate = await dbContext.Candidates.FindAsync(candidateId);

            if (candidate is null)
            {
                return Results.NotFound(new { message = "Candidate not found." });
            }

            dbContext.Candidates.Remove(candidate);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization("AdminOnly");


        return group;
    }
}