using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingSystem.api.Migrations
{
    /// <inheritdoc />
    public partial class AddView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW IF NOT EXISTS ElectionResults AS
                SELECT 
                    e.Id AS ElectionId, 
                    e.Title AS ElectionTitle, 
                    ep.Id AS ElectionPositionId,
                    p.Id AS PositionId,
                    p.Name AS PositionName,
                    c.Id AS CandidateId,  
                    u.Id AS CandidateUserId,  
                    COALESCE(u.Name, 'No candidate found') AS CandidateName, 
                    COALESCE(c.Party, 'Independent') AS Party, 
                    COALESCE(COUNT(v.Id), 0) AS TotalVotes,  
                    (SELECT COUNT(DISTINCT u_all.Id) FROM Users u_all) AS RegisteredVoters,  -- Get total users as voters
                    COALESCE(GROUP_CONCAT(vuser.Name, ', '), 'No Voter found') AS VoterNames  
                FROM Elections e
                LEFT JOIN ElectionPositions ep ON ep.ElectionId = e.Id
                LEFT JOIN Positions p ON ep.PositionId = p.Id
                LEFT JOIN Candidates c ON c.ElectionPositionId = ep.Id
                LEFT JOIN Users u ON c.UserId = u.Id
                LEFT JOIN Votes v ON v.CandidateId = c.Id AND v.ElectionPositionId = ep.Id  
                LEFT JOIN Users vuser ON v.UserId = vuser.Id  
                GROUP BY e.Id, e.Title, ep.Id, p.Id, p.Name, c.Id, u.Id, u.Name, c.Party;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
