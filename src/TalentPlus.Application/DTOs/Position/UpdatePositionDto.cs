using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Position
{
    public class UpdatePositionDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}