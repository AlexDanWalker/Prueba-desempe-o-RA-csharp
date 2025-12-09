using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Services
{
    public class EducationalLevelService : IEducationalLevelService
    {
        private readonly IEducationalLevelRepository _levelRepository;

        public EducationalLevelService(IEducationalLevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<EducationalLevel>> GetAllLevelsAsync()
        {
            return await _levelRepository.GetAllAsync();
        }

        public async Task<EducationalLevel?> GetLevelByIdAsync(int id)
        {
            return await _levelRepository.GetByIdAsync(id);
        }

        public async Task<EducationalLevel?> GetLevelByNameAsync(string name)
        {
            return await _levelRepository.GetByNameAsync(name);
        }

        public async Task AddLevelAsync(EducationalLevel level)
        {
            await _levelRepository.AddAsync(level);
        }

        public async Task UpdateLevelAsync(EducationalLevel level)
        {
            await _levelRepository.UpdateAsync(level);
        }

        public async Task DeleteLevelAsync(EducationalLevel level)
        {
            await _levelRepository.DeleteAsync(level);
        }
    }
}