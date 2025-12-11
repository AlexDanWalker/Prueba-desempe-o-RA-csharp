using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Application.DTOs.EducationalLevel;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]
    public class EducationalLevelController : ControllerBase
    {
        private readonly IEducationalLevelService _educationalLevelService;

        public EducationalLevelController(IEducationalLevelService educationalLevelService)
        {
            _educationalLevelService = educationalLevelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var levels = await _educationalLevelService.GetAllLevelsAsync();
            return Ok(levels); // Retorna la lista completa de niveles
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var level = await _educationalLevelService.GetLevelByIdAsync(id);
            if (level == null) return NotFound();
            return Ok(level); // Retorna un nivel específico
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEducationalLevelDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var level = new EducationalLevel
            {
                LevelName = createDto.Name
            };

            await _educationalLevelService.AddLevelAsync(level);
            return Ok(level); // Retorna el nivel recién creado
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEducationalLevelDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != updateDto.Id) return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");

            var existingLevel = await _educationalLevelService.GetLevelByIdAsync(id);
            if (existingLevel == null) return NotFound();

            existingLevel.LevelName = updateDto.Name;
            await _educationalLevelService.UpdateLevelAsync(existingLevel);

            return Ok(existingLevel); // Retorna el nivel actualizado
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var level = await _educationalLevelService.GetLevelByIdAsync(id);
            if (level == null) return NotFound();

            await _educationalLevelService.DeleteLevelAsync(level);
            return NoContent(); // Eliminación exitosa, sin contenido
        }
    }
}
