using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.api.Entities;
using System;

namespace OnlineVotingSystem.api.Data;

public class OnlineVotingSystemContext(DbContextOptions<OnlineVotingSystemContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Election> Elections => Set<Election>();
    public DbSet<ElectionPosition> ElectionPositions => Set<ElectionPosition>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Vote> Votes => Set<Vote>(); 

    //mapping view to entity
   public DbSet<ElectionResult> ElectionResults { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ElectionResult>().HasNoKey().ToView("ElectionResults");

        // Hardcoded GUIDs
        var user1Id = Guid.Parse("11111111-0000-1111-1111-111111111111");
        var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var electionId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        var positionPresidentId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var positionGovernorId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var senatorPositionId =new Guid( "2b3c4d5e-6f7a-8901-2345-abcdefabcdef");
        var mpPositionId = new Guid("3c4d5e6f-7a8b-9012-3456-abcdefabcdef");
        var womanRepPositionId = new Guid("4d5e6f7a-8b9c-0123-4567-abcdefabcdef");


        var electionPosition1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
        var electionPosition2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var electionPosition3Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var electionPosition4Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var electionPosition5Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        var candidate1Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var candidate2Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var candidate3Id = Guid.Parse("bbbbbbbb-bbbb-aaaa-bbbb-bbbbbbbbbbbb");
        var candidate4Id = Guid.Parse("cccccccc-cccc-aaaa-cccc-cccccccccccc");
        var candidate5Id = Guid.Parse("dddddddd-dddd-aaaa-dddd-dddddddddddd");

        var vote1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var vote2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var vote3Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var vote4Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var vote5Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        var vote6Id = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-ef0123456789");
        var vote7Id = Guid.Parse("b2c3d4e5-f6a7-4890-bcde-f01234567890");
        var vote8Id = Guid.Parse("c3d4e5f6-a7b8-4901-aaaa-012345678901");
        var vote9Id = Guid.Parse("d4e5f6a7-b8c9-4012-aaaa-123456789012");
        var vote10Id = Guid.Parse("e5f6a7b8-c9d0-4123-aaaa-234567890123");
        var vote11Id = Guid.Parse("f6a7b8c9-d0e1-4234-aaaa-345678901234");

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            //DefaultPassword!123
            new { Id = user1Id, NationalId = 12345678, Name = "John Kamau", Email = "john.kamau@example.com", Password = "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi", IsAdmin = false, CreatedAt = new DateTime(2024, 3, 1, 12, 0, 0) },
            new { Id = user2Id, NationalId = 87654321, Name = "Mary Njeri", Email = "mary.njeri@example.com", Password = "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi", IsAdmin = true, CreatedAt = new DateTime(2024, 3, 1, 12, 30, 0) },
            new { Id = user3Id, NationalId = 13579246, Name = "Kevin Otieno", Email = "kevin.otieno@example.com", Password = "$2a$11$RPcMfy2oo3vu3jUbdoanEe/KBs9GTo6BuS09XY4Dec8/1936ZgCOi", IsAdmin = false, CreatedAt = new DateTime(2024, 3, 1, 13, 0, 0) },
            // AdminPassword!1234
            new {Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Admin",NationalId = 10000001,Email = "admin@system.com",Password = "$2a$11$McDzAqqk04VkMTWMQmZnw.DR.uCHl/wj23tAKPZBJNSygN2koP8gK",IsAdmin = true,CreatedAt = new DateTime(2024, 2, 11, 12, 0, 0, DateTimeKind.Utc)},
            // Admin2Password!123
            new {Id = new Guid("11111111-2222-1111-1111-111111111111"), Name = "Admin",NationalId = 11110011,Email = "jon@admin.com",Password = "$2a$11$JlT0uwCu987Mw8SeJlwqnOlvkCilbvF3wNbOPR5PGqWcQkjDQRbd.", IsAdmin = true,CreatedAt = new DateTime(2024, 2, 11, 12, 0, 0, DateTimeKind.Utc)}
        );


        // Seed Election
        modelBuilder.Entity<Election>().HasData(
            new { Id = electionId, Title = "Kenya General Elections 2027", Description = "Presidential and parliamentary elections in Kenya", StartTime = new DateTime(2027, 8, 9, 6, 0, 0), EndTime = new DateTime(2027, 8, 9, 18, 0, 0), CreatedBy = user2Id }
        );


        // Seed Positions
        modelBuilder.Entity<Position>().HasData(
            new { Id = positionPresidentId, Name = "President" },
            new { Id = positionGovernorId, Name = "Governor" },
            new { Id = senatorPositionId, Name = "Senator" },
            new { Id = mpPositionId, Name = "Member of Parliament" },
            new { Id = womanRepPositionId, Name = "Women Representative" }
            
        );


        // Seed Election Positions
        modelBuilder.Entity<ElectionPosition>().HasData(
            new { Id = electionPosition1Id, ElectionId = electionId, PositionId = positionPresidentId },
            new { Id = electionPosition2Id, ElectionId = electionId, PositionId = positionGovernorId },
            new { Id = electionPosition3Id, ElectionId = electionId, PositionId = senatorPositionId },
            new { Id = electionPosition4Id, ElectionId = electionId, PositionId = mpPositionId },
            new { Id = electionPosition5Id, ElectionId = electionId, PositionId = womanRepPositionId }
        );

        // Seed Candidates
        modelBuilder.Entity<Candidate>().HasData(
            new { Id = candidate1Id, ElectionPositionId = electionPosition1Id, UserId = user1Id, Party = "Democratic Party of Kenya", Bio = "Experienced politician aiming for change.", PhotoUrl = new Uri("https://example.com/john_kamau.jpg")},
            new { Id = candidate2Id, ElectionPositionId = electionPosition1Id, UserId = user3Id, Party = "United Progressive Alliance", Bio = "Youth-driven leadership for a better tomorrow.", PhotoUrl = new Uri("https://example.com/kevin_otieno.jpg") },
            new { Id = candidate3Id, ElectionPositionId = electionPosition2Id, UserId = user1Id, Party = "Kenya African National Union", Bio = "Committed to serving the people.", PhotoUrl = new Uri("https://example.com/john_kamau.jpg") },
            new { Id = candidate4Id, ElectionPositionId = electionPosition2Id, UserId = user2Id, Party = "Kenya African National Union", Bio = "Committed to serving the people.", PhotoUrl = new Uri("https://example.com/mary_njeri.jpg") },
            new { Id = candidate5Id, ElectionPositionId = electionPosition3Id, UserId = user1Id, Party = "Kenya African National Union", Bio = "Committed to serving the people.", PhotoUrl = new Uri("https://example.com/john_kamau.jpg") }
            
        );

        // Seed Votes
        modelBuilder.Entity<Vote>().HasData(
            new { Id = vote1Id, UserId = user1Id, ElectionPositionId = electionPosition1Id, CandidateId = candidate1Id, Timestamp = new DateTime(2027, 8, 9, 8, 30, 0) },
            new { Id = vote2Id, UserId = user3Id, ElectionPositionId = electionPosition1Id, CandidateId = candidate2Id, Timestamp = new DateTime(2027, 8, 9, 9, 45, 0) },
            new { Id = vote3Id, UserId = user1Id, ElectionPositionId = electionPosition2Id, CandidateId = candidate3Id, Timestamp = new DateTime(2027, 8, 9, 10, 0, 0) },
            new { Id = vote4Id, UserId = user2Id, ElectionPositionId = electionPosition2Id, CandidateId = candidate4Id, Timestamp = new DateTime(2027, 8, 9, 11, 30, 0) },
            new { Id = vote5Id, UserId = user1Id, ElectionPositionId = electionPosition3Id, CandidateId = candidate5Id, Timestamp = new DateTime(2027, 8, 9, 12, 45, 0) },
            new { Id = vote6Id, UserId = user1Id, ElectionPositionId = electionPosition1Id, CandidateId = candidate1Id, Timestamp = new DateTime(2027, 8, 9, 13, 0, 0) },
            new { Id = vote7Id, UserId = user3Id, ElectionPositionId = electionPosition1Id, CandidateId = candidate2Id, Timestamp = new DateTime(2027, 8, 9, 14, 15, 0) },
            new { Id = vote8Id, UserId = user1Id, ElectionPositionId = electionPosition2Id, CandidateId = candidate3Id, Timestamp = new DateTime(2027, 8, 9, 15, 30, 0) },
            new { Id = vote9Id, UserId = user2Id, ElectionPositionId = electionPosition2Id, CandidateId = candidate4Id, Timestamp = new DateTime(2027, 8, 9, 16, 45, 0) },
            new { Id = vote10Id, UserId = user1Id, ElectionPositionId = electionPosition3Id, CandidateId = candidate5Id, Timestamp = new DateTime(2027, 8, 9, 17, 0, 0) }
        );


    }    
}
