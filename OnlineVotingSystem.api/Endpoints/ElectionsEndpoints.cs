using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.ElectionPosition;
using OnlineVotingSystem.api.Entities;
using OnlineVotingSystem.api.Mapping;

namespace OnlineVotingSystem.api.Endpoints;

public static class ElectionsEndpoints
{
    public static RouteGroupBuilder MapElectionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("elections").WithParameterValidation(); // Parent route

        // Get all elections - "http://localhost:PORT/elections/"
        group.MapGet("/", async (OnlineVotingSystemContext dbContext) =>
        {
            // select all elections
            return await dbContext.Elections
                .Select(election => election.ToElectionDetailsDto())
                .AsNoTracking()
                .ToListAsync();
        }).WithName("getElections"); // name of the route

        // Create election - "http://localhost:PORT/elections/create" - ADMIN ONLY
        group.MapPost("/create",
            async (
                CreateElectionDto newElection,
                OnlineVotingSystemContext dbContext,
                ClaimsPrincipal user
            ) =>
            {
                // get userId from JWT/ from logged-in user
                var userId = Guid.Parse(
                    user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var election = newElection.ToEntity(userId); // convert Json to entity data

                dbContext.Elections.Add(election); // add election to database
                await dbContext.SaveChangesAsync(); // save changes to the database

                return Results.CreatedAtRoute("getElections", new { id = election.Id },
                    election.ToElectionDetailsDto()); // return created election
            }).RequireAuthorization("AdminOnly");

        // Get election by id - "http://localhost:PORT/elections/{id}"
        group.MapGet("/{electionId:guid}", async (Guid electionId, OnlineVotingSystemContext dbContext) =>
        {
            var election = await dbContext.Elections.FindAsync(electionId); // find election by id
            // return null or the election when found
            return election is null ? Results.NotFound() : Results.Ok(election.ToElectionDetailsDto());
        });

        group.MapGet("/{electionId:guid}/positions", async (Guid electionId, OnlineVotingSystemContext dbContext) =>
        {
            return await dbContext.ElectionPositions
                .Where(ep => ep.ElectionId == electionId)
                .Include(election => election.Election)
                .Include(position => position.Position)
                .Select(electionPosition => electionPosition.ToElectionPositionSerializedDto())
                .AsNoTracking()
                .ToListAsync();
        });

        group.MapPost("/{electionId:guid}/positions",
            async (
                Guid electionId,
                CreateElectionPositionDto newElectionPosition,
                OnlineVotingSystemContext dbContext) =>
            {
                var electionExists = await dbContext.Elections.AnyAsync(e => e.Id == electionId);
                if (!electionExists) return Results.NotFound("Election not found.");

                // Validate if the position exists
                var positionExists = await dbContext.Positions.AnyAsync(
                    p => p.Id == newElectionPosition.PositionId);
                if (!positionExists) return Results.NotFound("Position not found.");


                // Check if the position is already linked to this election
                var existingEntry = await dbContext.ElectionPositions.AnyAsync(ep =>
                    ep.ElectionId == electionId && ep.PositionId == newElectionPosition.PositionId);
                if (existingEntry)
                    return Results.BadRequest("Position already added to this election");

                // Add to ElectionPositions table
                var electionPosition = newElectionPosition.ToEntity(electionId);

                dbContext.ElectionPositions.Add(electionPosition);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    "getElections", new { electionId = electionPosition.Id }
                );
            });

        group.MapPatch("/{electionId:guid}",
            async (Guid electionId, UpdateElectionDto updateDto, OnlineVotingSystemContext dbContext) =>
            {
                var election = await dbContext.Elections.FindAsync(electionId);
                if (election is null)
                {
                    return Results.NotFound("Position not found.");
                }

                election.ApplyUpdates(updateDto);
                await dbContext.SaveChangesAsync();

                return Results.Ok(election);
            });

        // Delete election by electionId - "http://localhost:PORT/elections/{electionId}" - ADMIN ONLY
        group.MapDelete("/{electionId:guid}", async (Guid electionId, OnlineVotingSystemContext dbContext) =>
        {
            // delete by electionId
            await dbContext.Elections.Where(election => election.Id == electionId).ExecuteDeleteAsync();

            return Results.NoContent(); // return no-content
        }).RequireAuthorization("AdminOnly");

        return group;
    }
}
