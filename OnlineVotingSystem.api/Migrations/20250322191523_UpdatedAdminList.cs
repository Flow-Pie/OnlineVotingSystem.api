using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVotingSystem.api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAdminList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "Name", "NationalId", "Password" },
                values: new object[] { new Guid("22222222-2222-2222-2222-111111111111"), new DateTime(2024, 2, 11, 12, 0, 0, 0, DateTimeKind.Utc), "jon@gmail.com", true, "Jon", 42424242, "$2a$11$.3uav6eCFqwMkvAQsdvQoeqIhGwly.MskOqZd9uGGHQomcPtagFkS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-111111111111"));
        }
    }
}
