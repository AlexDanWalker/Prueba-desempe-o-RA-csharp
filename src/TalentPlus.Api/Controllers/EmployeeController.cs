using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Application.DTOs.Employee;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            // Obtener todos los empleados desde el servicio
            var employees = await _employeeService.GetAllEmployeesAsync();

            // Mapear entidades a DTOs para no exponer navegación completa ni referencias circulares
            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Document = e.Document,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Address = e.Address,
                Phone = e.Phone,
                Email = e.Email,
                Salary = e.Salary,
                HireDate = e.HireDate,
                ProfessionalProfile = e.ProfessionalProfile,
                PositionId = e.PositionId,
                DepartmentId = e.DepartmentId,
                EducationalLevelId = e.EducationalLevelId
            });

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _employeeService.GetEmployeeByIdAsync(id);
            if (e == null) return NotFound();

            var dto = new EmployeeDto
            {
                Id = e.Id,
                Document = e.Document,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Address = e.Address,
                Phone = e.Phone,
                Email = e.Email,
                Salary = e.Salary,
                HireDate = e.HireDate,
                ProfessionalProfile = e.ProfessionalProfile,
                PositionId = e.PositionId,
                DepartmentId = e.DepartmentId,
                EducationalLevelId = e.EducationalLevelId
            };

            return Ok(dto);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            // Validación automática de los campos requeridos del DTO
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var employee = new Employee
            {
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                Salary = dto.Salary,
                HireDate = dto.HireDate,
                ProfessionalProfile = dto.ProfessionalProfile,
                PositionId = dto.PositionId,
                DepartmentId = dto.DepartmentId,
                EducationalLevelId = dto.EducationalLevelId
            };

            await _employeeService.AddEmployeeAsync(employee);

            // Retornamos solo el Id para evitar exponer referencias internas
            return Ok(new { employee.Id });
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            // Validación de campos requeridos
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validación de que el id de la URL y el DTO coincidan
            if (id != dto.Id) return BadRequest("Id mismatch");

            var existing = await _employeeService.GetEmployeeByIdAsync(id);
            if (existing == null) return NotFound();

            // Actualizar los campos del empleado
            existing.Document = dto.Document;
            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.BirthDate = dto.BirthDate;
            existing.Address = dto.Address;
            existing.Phone = dto.Phone;
            existing.Email = dto.Email;
            existing.Salary = dto.Salary;
            existing.HireDate = dto.HireDate;
            existing.ProfessionalProfile = dto.ProfessionalProfile;
            existing.PositionId = dto.PositionId;
            existing.DepartmentId = dto.DepartmentId;
            existing.EducationalLevelId = dto.EducationalLevelId;

            await _employeeService.UpdateEmployeeAsync(existing);

            // Usamos NoContent porque la actualización fue exitosa, no necesitamos enviar todo el objeto
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _employeeService.GetEmployeeByIdAsync(id);
            if (existing == null) return NotFound();

            await _employeeService.DeleteEmployeeAsync(existing);

            // No retornamos contenido porque la eliminación fue exitosa
            return NoContent();
        }
    }
}
