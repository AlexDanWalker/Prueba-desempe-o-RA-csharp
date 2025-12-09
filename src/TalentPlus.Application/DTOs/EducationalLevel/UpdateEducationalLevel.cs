using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.EducationalLevel
{
    public class UpdateEducationalLevelDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}