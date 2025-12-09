using QuestPDF.Fluent;
using QuestPDF.Helpers;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Domain.Entities;

namespace TalentPlus.Infrastructure.Pdf
{
    public class EmployeePdfGenerator : IEmployeePdfGenerator
    {
        public async Task<byte[]> GenerateEmployeeCvAsync(Employee employee)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    
                    page.Header().Element(header =>
                    {
                        header.AlignCenter();
                        header.Text($"Employee CV: {employee.FirstName} {employee.LastName}")
                              .FontSize(20)
                              .SemiBold();
                    });

                    page.Content().Column(column =>
                    {
                        column.Spacing(5);

                        column.Item().Text($"Document: {employee.Document}");
                        column.Item().Text($"Email: {employee.Email}");
                        column.Item().Text($"Phone: {employee.Phone}");
                        column.Item().Text($"Address: {employee.Address}");
                        column.Item().Text($"Birth Date: {employee.BirthDate:yyyy-MM-dd}");

                        column.Item().Text($"Position: {employee.Position?.Name}");
                        column.Item().Text($"Department: {employee.Department?.Name}");
                        column.Item().Text($"Salary: {employee.Salary:C}");
                        column.Item().Text($"Hire Date: {employee.HireDate:yyyy-MM-dd}");
                        column.Item().Text($"State: {employee.State}");

                        column.Item().Text($"Educational Level: {employee.EducationalLevel?.LevelName}");
                        column.Item().Text($"Professional Profile: {employee.ProfessionalProfile}");
                    });
                });
            });

            using var ms = new MemoryStream();
            document.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}
