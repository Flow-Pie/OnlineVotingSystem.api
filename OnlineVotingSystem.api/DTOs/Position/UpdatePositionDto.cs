using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs.Position;

public record UpdatePositionDto(
    [StringLength(255)] string Name
);