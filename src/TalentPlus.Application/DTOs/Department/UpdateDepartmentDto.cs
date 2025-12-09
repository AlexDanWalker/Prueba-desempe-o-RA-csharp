using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}