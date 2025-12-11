using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Domain.Entities;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Application.DTOs.Department;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("public")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetPublic()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            // Convertimos a DTO para no exponer la entidad completa
            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });
            return Ok(departmentDtos);
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });
            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();

            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(departmentDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var department = new Department
            {
                Name = createDto.Name
            };

            await _departmentService.AddDepartmentAsync(department);

            // Retornamos un DTO en lugar de la entidad completa
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(departmentDto);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _departmentService.GetDepartmentByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = updateDto.Name;

            await _departmentService.UpdateDepartmentAsync(existing);

            var departmentDto = new DepartmentDto
            {
                Id = existing.Id,
                Name = existing.Name
            };

            return Ok(departmentDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _departmentService.GetDepartmentByIdAsync(id);
            if (existing == null) return NotFound();

            await _departmentService.DeleteDepartmentAsync(existing);
            return NoContent();
        }
    }
}
