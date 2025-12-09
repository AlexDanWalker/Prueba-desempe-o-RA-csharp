using Microsoft.EntityFrameworkCore;
using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Domain.Entities;
using TalentPlus.Infrastructure.Data;

namespace TalentPlus.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TalentPlusDbContext _context;

        public EmployeeRepository(TalentPlusDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.EducationalLevel)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetByDocumentAsync(string document)
        {
            return await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.EducationalLevel)
                .FirstOrDefaultAsync(e => e.Document == document);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.EducationalLevel)
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}