using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingSystem.api.Migrations
{
    /// <inheritdoc />
    public partial class FixedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "Name", "NationalId", "Password" },
                values: new object[] { new Guid("11111111-2222-1111-1111-111111111111"), new DateTime(2024, 2, 11, 12, 0, 0, 0, DateTimeKind.Utc), "jon@admin.com", true, "Admin", 11110011, "$2a$11$JlT0uwCu987Mw8SeJlwqnOlvkCilbvF3wNbOPR5PGqWcQkjDQRbd." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-1111-1111-111111111111"));
        }
    }
}
