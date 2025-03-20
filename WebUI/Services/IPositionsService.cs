using OnlineVotingSystem.api.DTOs.Election;
using OnlineVotingSystem.api.DTOs.Position;

namespace WebUI.Services;

public interface IPositionsService
{
    Task<IEnumerable<PositionDetails>> GetPositionsAsync();
    Task<PositionDetails> CreatePositionAsync(CreatePositionDto  createPositionDto, string token);
}