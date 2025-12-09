using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Repositories;

    public interface IPositionRepository
    {
        Task<Position?> GetByIdAsync(int id);
        Task<Position?> GetByNameAsync(string name);
        Task<IEnumerable<Position>> GetAllAsync();
        Task AddAsync(Position position);
        Task UpdateAsync(Position position);
        Task DeleteAsync(Position position);
    }
