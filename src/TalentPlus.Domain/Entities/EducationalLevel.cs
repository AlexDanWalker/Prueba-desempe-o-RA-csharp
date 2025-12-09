namespace TalentPlus.Domain.Entities;

public class EducationalLevel
{
    public int Id { get; set; }
    public string LevelName { get; set; } = string.Empty;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}