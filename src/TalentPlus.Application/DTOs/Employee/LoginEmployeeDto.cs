using System.ComponentModel.DataAnnotations;

namespace TalentPlus.Application.DTOs.Employee
{
    public class LoginEmployeeDto
    {
        [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}