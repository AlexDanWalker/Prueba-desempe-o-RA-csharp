using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentPlus.Application.DTOs.Position;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]
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
            
            var dtos = positions.Select(p => new PositionDto
            {
                Id = p.Id,
                Name = p.Name
            });

            return Ok(dtos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null) return NotFound();

            var dto = new PositionDto
            {
                Id = position.Id,
                Name = position.Name
            };

            return Ok(dto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePositionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var position = new Position
            {
                Name = dto.Name
            };

            await _positionService.AddPositionAsync(position);


            var resultDto = new PositionDto
            {
                Id = position.Id,
                Name = position.Name
            };

            return Ok(resultDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePositionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id) return BadRequest();

            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null) return NotFound();

            position.Name = dto.Name;

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
