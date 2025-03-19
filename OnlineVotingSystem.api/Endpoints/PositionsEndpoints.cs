using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs.Position;
using OnlineVotingSystem.api.Mapping;

namespace OnlineVotingSystem.api.Endpoints;

public static class PositionsEndpoints
{
    public static RouteGroupBuilder MapPositionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("positions").WithParameterValidation();

        group.MapGet("/", async (OnlineVotingSystemContext dbContext) =>
        {
            return await dbContext.Positions
                .Select(position => position.ToPositionDetails())
                .AsNoTracking()
                .ToListAsync();
        }).WithName("getPositions");

        group.MapGet("/{positionId:guid}",
            async (Guid positionId, OnlineVotingSystemContext dbContext) =>
            {
                var position = await dbContext.Positions.FindAsync(positionId);
                return
                    position is null
                        ? Results.NotFound()
                        : Results.Ok(position.ToPositionDetails());
            });

        group.MapPatch("/{positionId:guid}", async (Guid positionId, UpdatePositionDto updateDto, OnlineVotingSystemContext dbContext) =>
        {
            var position = await dbContext.Positions.FindAsync(positionId);
            if (position is null)
            {
                return Results.NotFound("Position not found.");
            }

            position.Name = updateDto.Name;
            await dbContext.SaveChangesAsync();

            return Results.Ok(position);
        });

        group.MapPost("/create",
            async (CreatePositionDto newPosition,
                OnlineVotingSystemContext dbContext) =>
            {
                var position = newPosition.ToEntity();

                dbContext.Positions.Add(position);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute("getPositions",
                    new { positionId = position.Id },
                    position.ToPositionDetails());
            }).RequireAuthorization("AdminOnly");

        group.MapDelete("/{positionId:guid}",
            async (Guid positionId, OnlineVotingSystemContext dbContext) =>
            {
                await dbContext.Positions
                    .Where(position => position.Id == positionId)
                    .ExecuteDeleteAsync();

                return Results.NoContent();
            }).RequireAuthorization("AdminOnly");

        return group;
    }
}