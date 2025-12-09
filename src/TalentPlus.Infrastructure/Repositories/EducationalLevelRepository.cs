using Microsoft.EntityFrameworkCore;
using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Domain.Entities;
using TalentPlus.Infrastructure.Data;

namespace TalentPlus.Infrastructure.Repositories
{
    public class EducationalLevelRepository : IEducationalLevelRepository
    {
        private readonly TalentPlusDbContext _context;

        public EducationalLevelRepository(TalentPlusDbContext context)
        {
            _context = context;
        }

        public async Task<EducationalLevel?> GetByIdAsync(int id)
        {
            return await _context.EducationalLevels
                .Include(el => el.Employees)
                .FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<EducationalLevel?> GetByNameAsync(string name)
        {
            return await _context.EducationalLevels
                .Include(el => el.Employees)
                .FirstOrDefaultAsync(el => el.LevelName == name);
        }

        public async Task<IEnumerable<EducationalLevel>> GetAllAsync()
        {
            return await _context.EducationalLevels
                .Include(el => el.Employees)
                .ToListAsync();
        }

        public async Task AddAsync(EducationalLevel level)
        {
            await _context.EducationalLevels.AddAsync(level);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EducationalLevel level)
        {
            _context.EducationalLevels.Update(level);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EducationalLevel level)
        {
            _context.EducationalLevels.Remove(level);
            await _context.SaveChangesAsync();
        }
    }
}