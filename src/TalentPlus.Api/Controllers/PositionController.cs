using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var positions = await _positionService.GetAllPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null) return NotFound();
            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Position position)
        {
            await _positionService.AddPositionAsync(position);
            return Ok(position);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Position position)
        {
            if (id != position.Id) return BadRequest();
            await _positionService.UpdatePositionAsync(position);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null) return NotFound();

            await _positionService.DeletePositionAsync(position);
            return NoContent();
        }
    }
}