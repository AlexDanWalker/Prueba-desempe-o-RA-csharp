using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.EducationalLevel
{
    public class CreateEducationalLevelDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}