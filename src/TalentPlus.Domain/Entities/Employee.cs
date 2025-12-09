using TalentPlus.Domain.Enums;

namespace TalentPlus.Domain.Entities
{
    public class Employee
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

        public EmployeeState State { get; set; }

        public string ProfessionalProfile { get; set; } = string.Empty;
        
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public int EducationalLevelId { get; set; }
        
        public Position Position { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public EducationalLevel EducationalLevel { get; set; } = null!;
    }
}