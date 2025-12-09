using Microsoft.EntityFrameworkCore;
using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Domain.Entities;
using TalentPlus.Infrastructure.Data;

namespace TalentPlus.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly TalentPlusDbContext _context;

        public PositionRepository(TalentPlusDbContext context)
        {
            _context = context;
        }

        public async Task<Position?> GetByIdAsync(int id)
        {
            return await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Position?> GetByNameAsync(string name)
        {
            return await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await _context.Positions
                .Include(p => p.Employees)
                .ToListAsync();
        }

        public async Task AddAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Position position)
        {
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Position position)
        {
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }
    }
}