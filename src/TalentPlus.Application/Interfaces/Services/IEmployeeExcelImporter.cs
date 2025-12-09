using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Services
{
    public interface IEmployeeExcelImporter
    {
        /// <summary>
        /// Import employees from an Excel stream.
        /// </summary>
        /// <param name="excelStream">The Excel file stream.</param>
        /// <returns>A list of employees imported.</returns>
        Task<IEnumerable<Employee>> ImportAsync(Stream excelStream);
    }
}