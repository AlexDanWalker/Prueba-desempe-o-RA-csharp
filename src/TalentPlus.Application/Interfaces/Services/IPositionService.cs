using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<Position?> GetPositionByIdAsync(int id);
        Task<Position?> GetPositionByNameAsync(string name);
        Task AddPositionAsync(Position position);
        Task UpdatePositionAsync(Position position);
        Task DeletePositionAsync(Position position);
    }
}