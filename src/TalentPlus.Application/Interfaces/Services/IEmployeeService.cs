using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        // CRUD básico
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee?> GetEmployeeByDocumentAsync(string document);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);

        // Importación desde Excel
        Task<IEnumerable<Employee>> ImportFromExcelAsync(Stream fileStream);

        // Generación de PDF de hoja de vida
        Task<byte[]>? GeneratePdfAsync(int employeeId);
        Task<byte[]>? GeneratePdfByUserIdAsync(string userId);

        // (Opcional, si decides manejar email desde servicio de empleados)
        Task SendEmailAsync(string to, string subject, string body);
    }
}