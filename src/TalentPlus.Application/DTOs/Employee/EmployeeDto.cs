namespace TalentPlus.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Document { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string ProfessionalProfile { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public int EducationalLevelId { get; set; }
    }
}