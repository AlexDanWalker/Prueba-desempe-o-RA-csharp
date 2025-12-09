using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Document { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public string ProfessionalProfile { get; set; } = string.Empty;

        [Required]
        public int PositionId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int EducationalLevelId { get; set; }
    }
}