using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await _positionRepository.GetAllAsync();
        }

        public async Task<Position?> GetPositionByIdAsync(int id)
        {
            return await _positionRepository.GetByIdAsync(id);
        }

        public async Task<Position?> GetPositionByNameAsync(string name)
        {
            return await _positionRepository.GetByNameAsync(name);
        }

        public async Task AddPositionAsync(Position position)
        {
            await _positionRepository.AddAsync(position);
        }

        public async Task UpdatePositionAsync(Position position)
        {
            await _positionRepository.UpdateAsync(position);
        }

        public async Task DeletePositionAsync(Position position)
        {
            await _positionRepository.DeleteAsync(position);
        }
    }
}