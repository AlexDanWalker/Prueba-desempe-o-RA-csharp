using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Employee
{
    public class RegisterEmployeeDto
    {
        [Required] public string Document { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}