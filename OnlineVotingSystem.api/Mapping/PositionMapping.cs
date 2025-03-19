using OnlineVotingSystem.api.DTOs.Position;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class PositionMapping
{
    public static PositionDetails ToPositionDetails(this Position position)
    {
        return new PositionDetails(
            position.Id,
            position.Name
        );
    }

    public static Position ToEntity(this CreatePositionDto position)
    {
        return new Position
        {
            Name = position.Name
        };
    }
}