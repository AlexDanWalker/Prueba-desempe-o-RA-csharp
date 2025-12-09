using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
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
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var level = await _educationalLevelService.GetLevelByIdAsync(id);
            if (level == null) return NotFound();
            return Ok(level);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationalLevel level)
        {
            await _educationalLevelService.AddLevelAsync(level);
            return Ok(level);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EducationalLevel level)
        {
            if (id != level.Id) return BadRequest();
            await _educationalLevelService.UpdateLevelAsync(level);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var level = await _educationalLevelService.GetLevelByIdAsync(id);
            if (level == null) return NotFound();

            await _educationalLevelService.DeleteLevelAsync(level);
            return NoContent();
        }
    }
}