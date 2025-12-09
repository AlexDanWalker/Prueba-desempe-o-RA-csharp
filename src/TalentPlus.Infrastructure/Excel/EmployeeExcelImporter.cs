using System.Globalization;
using OfficeOpenXml;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;
using TalentPlus.Domain.Enums;

namespace TalentPlus.Infrastructure.Excel
{
    public class EmployeeExcelImporter : IEmployeeExcelImporter
    {
        public async Task<IEnumerable<Employee>> ImportAsync(Stream excelStream)
        {
            var employees = new List<Employee>();

            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0]; 

            if (worksheet.Dimension == null) return employees;

            var rowCount = worksheet.Dimension.Rows;
            
            for (int row = 2; row <= rowCount; row++) 
            {
                try
                {
                    var employee = new Employee
                    {
                        Document = worksheet.Cells[row, 1].Text.Trim(),
                        FirstName = worksheet.Cells[row, 2].Text.Trim(),
                        LastName = worksheet.Cells[row, 3].Text.Trim(),
                        BirthDate = DateTime.ParseExact(worksheet.Cells[row, 4].Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Address = worksheet.Cells[row, 5].Text.Trim(),
                        Phone = worksheet.Cells[row, 6].Text.Trim(),
                        Email = worksheet.Cells[row, 7].Text.Trim(),
                        Salary = decimal.Parse(worksheet.Cells[row, 9].Text.Trim(), CultureInfo.InvariantCulture),
                        HireDate = DateTime.ParseExact(worksheet.Cells[row, 10].Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        State = Enum.Parse<EmployeeState>(worksheet.Cells[row, 11].Text.Trim()),
                        ProfessionalProfile = worksheet.Cells[row, 13].Text.Trim(),
                        PositionId = int.Parse(worksheet.Cells[row, 8].Text.Trim()),
                        DepartmentId = int.Parse(worksheet.Cells[row, 14].Text.Trim()),
                        EducationalLevelId = int.Parse(worksheet.Cells[row, 12].Text.Trim())
                    };

                    employees.Add(employee);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error importing row {row}: {ex.Message}");
                }
            }

            return employees;
        }
    }
}
