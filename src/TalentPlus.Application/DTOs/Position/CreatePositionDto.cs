using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Position
{
    public class CreatePositionDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}