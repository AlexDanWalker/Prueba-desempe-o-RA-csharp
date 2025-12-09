
using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;
using TalentPlus.Infrastructure.Email;

namespace TalentPlus.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeExcelImporter _excelImporter;
        private readonly IEmployeePdfGenerator _pdfGenerator;
        private readonly IEmailService _emailService;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IEmployeeExcelImporter excelImporter,
            IEmployeePdfGenerator pdfGenerator,
            IEmailService emailService)
        {
            _employeeRepository = employeeRepository;
            _excelImporter = excelImporter;
            _pdfGenerator = pdfGenerator;
            _emailService = emailService;
        }

        // 🔹 CRUD básico
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
            => await _employeeRepository.GetAllAsync();

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
            => await _employeeRepository.GetByIdAsync(id);

        public async Task<Employee?> GetEmployeeByDocumentAsync(string document)
            => await _employeeRepository.GetByDocumentAsync(document);

        public async Task AddEmployeeAsync(Employee employee)
            => await _employeeRepository.AddAsync(employee);

        public async Task UpdateEmployeeAsync(Employee employee)
            => await _employeeRepository.UpdateAsync(employee);

        public async Task DeleteEmployeeAsync(Employee employee)
            => await _employeeRepository.DeleteAsync(employee);
        
        public async Task<IEnumerable<Employee>> ImportFromExcelAsync(Stream excelStream)
        {
            if (excelStream == null || excelStream.Length == 0)
                return Enumerable.Empty<Employee>();

            var employees = await _excelImporter.ImportAsync(excelStream);

            foreach (var emp in employees)
                await AddEmployeeAsync(emp);  // guarda en DB

            return employees;
        }
        
        public async Task<byte[]?> GeneratePdfAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return null;

            return await _pdfGenerator.GenerateEmployeeCvAsync(employee);
        }

        public async Task<byte[]?> GeneratePdfByUserIdAsync(string userId)
        {
            if (!int.TryParse(userId, out int employeeId))
                return null;

            return await GeneratePdfAsync(employeeId);
        }
        
        public async Task SendEmailAsync(string to, string subject, string body)
            => await _emailService.SendEmailAsync(to, subject, body);
    }
}
