using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs.User;
using OnlineVotingSystem.api.Mapping;
using OnlineVotingSystem.api.Services;

namespace OnlineVotingSystem.api.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("auth").WithParameterValidation();

        // register
        group.MapPost("/register", async (CreateUserDto newUser, OnlineVotingSystemContext dbContext) =>
        {
            if (await dbContext.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                return Results.BadRequest("Email already in use.");
            }

            if (await dbContext.Users.AnyAsync(u => u.NationalId == newUser.NationalId))
            {
                return Results.BadRequest("NationalId number already registered.");
            }

            var user = newUser.ToEntity();

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                "getUsers", new { id = user.Id }, user.ToUserDetailsDto()
            );
        });

        // login
        group.MapPost("/login",
            async (LoginUserDto login, JwtService jwtService) =>
            {
                var result = await jwtService.Authenticate(login);

                return result is null ? Results.Unauthorized() : Results.Ok(result);
            });

        return group;
    }
}
