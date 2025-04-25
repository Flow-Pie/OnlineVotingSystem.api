using WebUI.DTOs.Election;
using WebUI.DTOs.Position;

namespace WebUI.Services;

public interface IPositionsService
{
    Task<IEnumerable<PositionDetails>> GetPositionsAsync();
    Task<PositionDetails> CreatePositionAsync(CreatePositionDto  createPositionDto, string token);
}