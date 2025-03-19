using Microsoft.EntityFrameworkCore;

namespace OnlineVotingSystem.api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OnlineVotingSystemContext>();
        await dbContext.Database.MigrateAsync();
    }
}