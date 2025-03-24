using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Data;

namespace OnlineVotingSystem.api.Services;

public class ViewManagerService
{
    private readonly OnlineVotingSystemContext _context;

    public ViewManagerService(OnlineVotingSystemContext context)
    {
        _context = context;
    }

    public void EnsureViewsCreated()
    {
        var sql = @"
            CREATE VIEW IF NOT EXISTS ElectionResults AS
            SELECT 
                c.Id AS CandidateId, 
                c.UserId AS CandidateUserId, 
                u.Name AS CandidateName, 
                c.Party, 
                ep.Id AS ElectionPositionId,  
                ep.PositionId, 
                p.Name AS PositionName,
                e.Id AS ElectionId, 
                e.Title AS ElectionTitle, 
                COALESCE(COUNT(v.Id), 0) AS TotalVotes  
            FROM Candidates c
            JOIN Users u ON c.UserId = u.Id
            JOIN ElectionPositions ep ON c.ElectionPositionId = ep.Id
            JOIN Positions p ON ep.PositionId = p.Id
            JOIN Elections e ON ep.ElectionId = e.Id
            LEFT JOIN Votes v ON v.CandidateId = c.Id AND v.ElectionPositionId = ep.Id  
            GROUP BY c.Id, c.UserId, u.Name, c.Party, ep.Id, ep.PositionId, p.Name, e.Id, e.Title;
        ";

        _context.Database.ExecuteSqlRaw(sql);
    }
}
