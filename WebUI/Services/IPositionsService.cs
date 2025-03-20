using OnlineVotingSystem.api.DTOs.Position;

namespace WebUI.Services;

public interface IPositionsService
{
    Task<IEnumerable<PositionDetails>> GetPositionsAsync();
}