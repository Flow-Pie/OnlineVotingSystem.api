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
                e.Id AS ElectionId, 
                e.Title AS ElectionTitle, 
                ep.Id AS ElectionPositionId,
                p.Id AS PositionId,
                p.Name AS PositionName,
                COALESCE(c.Id, '') AS CandidateId, 
                COALESCE(u.Id, '') AS CandidateUserId, 
                COALESCE(u.Name, 'Unknown') AS CandidateName, 
                COALESCE(c.Party, 'Independent') AS Party, 
                COALESCE(COUNT(v.Id), 0) AS TotalVotes,  
                COALESCE((SELECT COUNT(DISTINCT v.UserId) 
                        FROM Votes v 
                        WHERE v.ElectionPositionId = ep.Id), 0) AS RegisteredVoters,
                COALESCE(GROUP_CONCAT(vuser.Name, ', '), 'No Votes') AS VoterNames  
            FROM Elections e
            LEFT JOIN ElectionPositions ep ON ep.ElectionId = e.Id
            LEFT JOIN Positions p ON ep.PositionId = p.Id
            LEFT JOIN Candidates c ON c.ElectionPositionId = ep.Id
            LEFT JOIN Users u ON c.UserId = u.Id
            LEFT JOIN Votes v ON v.CandidateId = c.Id AND v.ElectionPositionId = ep.Id  
            LEFT JOIN Users vuser ON v.UserId = vuser.Id  
            GROUP BY e.Id, e.Title, ep.Id, p.Id, p.Name, c.Id, u.Id, u.Name, c.Party;


        ";

        _context.Database.ExecuteSqlRaw(sql);
    }
}
