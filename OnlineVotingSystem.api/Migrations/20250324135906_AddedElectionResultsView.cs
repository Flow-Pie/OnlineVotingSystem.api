using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingSystem.api.Migrations
{
    /// <inheritdoc />
    public partial class AddedElectionResultsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql(@"
                CREATE VIEW ElectionResults AS
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
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("DROP VIEW IF EXISTS ElectionResults;");
        }
    }
}
