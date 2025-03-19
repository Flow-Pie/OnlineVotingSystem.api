using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.Mapping;

namespace OnlineVotingSystem.api.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users").WithParameterValidation();

        // Get all users
        group.MapGet("/", async (OnlineVotingSystemContext dbContext) =>
        {
            return await dbContext.Users
                .Select(user => user.ToUserDetailsDto())
                .AsNoTracking()
                .ToListAsync();
        }).WithName("getUsers");

        // Get specific User by id
        group.MapGet("/{id:guid}", async (Guid id, OnlineVotingSystemContext dbContext) =>
        {
            var user = await dbContext.Users.FindAsync(id);
            return user is null ? Results.NotFound() : Results.Ok(user.ToUserDetailsDto());
        });

        // NUKE users
        group.MapDelete("/nuke", async (HttpContext context, OnlineVotingSystemContext dbContext) =>
        {
            var confirm = context.Request.Query["confirm"].ToString();

            if (!confirm.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
            {
                return Results.BadRequest("Launch codes required");
            }

            await dbContext.Users.ExecuteDeleteAsync();
            return Results.Ok("NO SURVIVORS");
        }).RequireAuthorization("AdminOnly");

        // delete user by id
        group.MapDelete("/{id:guid}", async (Guid id, OnlineVotingSystemContext dbContext) =>
        {
            await dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        }).RequireAuthorization("AdminOnly");
        return group;
    }
}