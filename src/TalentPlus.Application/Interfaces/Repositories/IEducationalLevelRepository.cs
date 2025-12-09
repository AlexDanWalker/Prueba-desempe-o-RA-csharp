using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Repositories;

public interface IEducationalLevelRepository
{
    Task<EducationalLevel?> GetByIdAsync(int id);
    Task<EducationalLevel?> GetByNameAsync(string name);
    Task<IEnumerable<EducationalLevel>> GetAllAsync();
    Task AddAsync(EducationalLevel level);
    Task UpdateAsync(EducationalLevel level);
    Task DeleteAsync(EducationalLevel level);
}