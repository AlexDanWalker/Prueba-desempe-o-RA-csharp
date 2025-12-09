using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}