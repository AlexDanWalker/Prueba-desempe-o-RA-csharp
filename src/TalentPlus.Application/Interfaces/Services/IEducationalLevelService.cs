using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Services
{
    public interface IEducationalLevelService
    {
        Task<IEnumerable<EducationalLevel>> GetAllLevelsAsync();
        Task<EducationalLevel?> GetLevelByIdAsync(int id);
        Task<EducationalLevel?> GetLevelByNameAsync(string name);
        Task AddLevelAsync(EducationalLevel level);
        Task UpdateLevelAsync(EducationalLevel level);
        Task DeleteLevelAsync(EducationalLevel level);
    }
}