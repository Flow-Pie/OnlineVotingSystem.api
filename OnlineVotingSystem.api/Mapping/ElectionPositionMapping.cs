using OnlineVotingSystem.api.DTOs.ElectionPosition;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class ElectionPositionMapping
{
    public static ElectionPositionSerialized ToElectionPositionSerializedDto(this ElectionPosition electionPosition)
    {
        return new ElectionPositionSerialized(
            electionPosition.Id,
            electionPosition.Election!.Title,
            electionPosition.Position!.Name
        );
    }

    public static ElectionPosition ToEntity(this CreateElectionPositionDto electionPosition, Guid electionId)
    {
        return new ElectionPosition
        {
            Id = Guid.NewGuid(),
            ElectionId = electionId,
            PositionId = electionPosition.PositionId
        };
    }
}