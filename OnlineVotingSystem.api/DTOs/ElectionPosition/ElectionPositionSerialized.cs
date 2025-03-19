namespace OnlineVotingSystem.api.DTOs.ElectionPosition;

public record ElectionPositionSerialized(
    Guid Id,
    string Election,
    string Position
);