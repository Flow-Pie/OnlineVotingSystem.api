using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineVotingSystem.api.Migrations
{
    /// <inheritdoc />
    public partial class freshMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NationalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elections_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectionPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ElectionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PositionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionPositions_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ElectionPositionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Party = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Bio = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PhotoUrl = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_ElectionPositions_ElectionPositionId",
                        column: x => x.ElectionPositionId,
                        principalTable: "ElectionPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ElectionPositionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CandidateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_ElectionPositions_ElectionPositionId",
                        column: x => x.ElectionPositionId,
                        principalTable: "ElectionPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b3c4d5e-6f7a-8901-2345-abcdefabcdef"), "Senator" },
                    { new Guid("3c4d5e6f-7a8b-9012-3456-abcdefabcdef"), "Member of Parliament" },
                    { new Guid("4d5e6f7a-8b9c-0123-4567-abcdefabcdef"), "Women Representative" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "President" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Governor" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsAdmin", "Name", "NationalId", "Password" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-1111-1111-111111111111"), new DateTime(2024, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "john.kamau@example.com", false, "John Kamau", 12345678, "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 2, 11, 12, 0, 0, 0, DateTimeKind.Utc), "admin@system.com", true, "Admin", 10000001, "$2a$11$McDzAqqk04VkMTWMQmZnw.DR.uCHl/wj23tAKPZBJNSygN2koP8gK" },
                    { new Guid("11111111-2222-1111-1111-111111111111"), new DateTime(2024, 2, 11, 12, 0, 0, 0, DateTimeKind.Utc), "jon@admin.com", true, "Admin", 11110011, "$2a$11$JlT0uwCu987Mw8SeJlwqnOlvkCilbvF3wNbOPR5PGqWcQkjDQRbd." },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 3, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), "mary.njeri@example.com", true, "Mary Njeri", 87654321, "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 3, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "kevin.otieno@example.com", false, "Kevin Otieno", 13579246, "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi" }
                });

            migrationBuilder.InsertData(
                table: "Elections",
                columns: new[] { "Id", "CreatedBy", "Description", "EndTime", "StartTime", "Title" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222"), "Presidential and parliamentary elections in Kenya", new DateTime(2027, 8, 9, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 8, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), "Kenya General Elections 2027" });

            migrationBuilder.InsertData(
                table: "ElectionPositions",
                columns: new[] { "Id", "ElectionId", "PositionId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("2b3c4d5e-6f7a-8901-2345-abcdefabcdef") },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("3c4d5e6f-7a8b-9012-3456-abcdefabcdef") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("4d5e6f7a-8b9c-0123-4567-abcdefabcdef") }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Bio", "ElectionPositionId", "Party", "PhotoUrl", "UserId" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Experienced politician aiming for change.", new Guid("77777777-7777-7777-7777-777777777777"), "Democratic Party of Kenya", "https://example.com/john_kamau.jpg", new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Youth-driven leadership for a better tomorrow.", new Guid("77777777-7777-7777-7777-777777777777"), "United Progressive Alliance", "https://example.com/kevin_otieno.jpg", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-bbbb-aaaa-bbbb-bbbbbbbbbbbb"), "Committed to serving the people.", new Guid("88888888-8888-8888-8888-888888888888"), "Kenya African National Union", "https://example.com/john_kamau.jpg", new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("cccccccc-cccc-aaaa-cccc-cccccccccccc"), "Committed to serving the people.", new Guid("88888888-8888-8888-8888-888888888888"), "Kenya African National Union", "https://example.com/mary_njeri.jpg", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("dddddddd-dddd-aaaa-dddd-dddddddddddd"), "Committed to serving the people.", new Guid("99999999-9999-9999-9999-999999999999"), "Kenya African National Union", "https://example.com/john_kamau.jpg", new Guid("11111111-0000-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "CandidateId", "ElectionPositionId", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2027, 8, 9, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("b2c3d4e5-f6a7-4890-bcde-f01234567890"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2027, 8, 9, 14, 15, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2027, 8, 9, 8, 30, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("c3d4e5f6-a7b8-4901-aaaa-012345678901"), new Guid("bbbbbbbb-bbbb-aaaa-bbbb-bbbbbbbbbbbb"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2027, 8, 9, 15, 30, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2027, 8, 9, 9, 45, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("d4e5f6a7-b8c9-4012-aaaa-123456789012"), new Guid("cccccccc-cccc-aaaa-cccc-cccccccccccc"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2027, 8, 9, 16, 45, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("bbbbbbbb-bbbb-aaaa-bbbb-bbbbbbbbbbbb"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2027, 8, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("e5f6a7b8-c9d0-4123-aaaa-234567890123"), new Guid("dddddddd-dddd-aaaa-dddd-dddddddddddd"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2027, 8, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("cccccccc-cccc-aaaa-cccc-cccccccccccc"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2027, 8, 9, 11, 30, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("dddddddd-dddd-aaaa-dddd-dddddddddddd"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2027, 8, 9, 12, 45, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-0000-1111-1111-111111111111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ElectionPositionId",
                table: "Candidates",
                column: "ElectionPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserId",
                table: "Candidates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPositions_ElectionId",
                table: "ElectionPositions",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPositions_PositionId",
                table: "ElectionPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Elections_CreatedBy",
                table: "Elections",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ElectionPositionId",
                table: "Votes",
                column: "ElectionPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "ElectionPositions");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
