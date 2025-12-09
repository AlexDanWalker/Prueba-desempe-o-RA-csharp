using TalentPlus.Application.DTOs.Employee;

namespace TalentPlus.Application.Interfaces
{
    public interface IAuthService
    {
        // Registro de empleado (autoregistro)
        Task<EmployeeDto> RegisterAsync(RegisterEmployeeDto dto);

        // Login de empleado, devuelve JWT
        Task<string> LoginAsync(LoginEmployeeDto dto);
    }
}