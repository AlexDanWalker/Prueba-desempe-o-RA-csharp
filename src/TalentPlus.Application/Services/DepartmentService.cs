using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task<Department?> GetDepartmentByNameAsync(string name)
        {
            return await _departmentRepository.GetByNameAsync(name);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _departmentRepository.AddAsync(department);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            await _departmentRepository.DeleteAsync(department);
        }
    }
}