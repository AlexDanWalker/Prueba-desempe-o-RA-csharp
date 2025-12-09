using TalentPlus.Domain.Entities;

namespace TalentPlus.Application.Interfaces.Services
{
    public interface IEmployeePdfGenerator
    {
        /// <summary>
        /// Generate a PDF of the employee's CV.
        /// </summary>
        /// <param name="employee">The employee entity.</param>
        /// <returns>Byte array of the generated PDF.</returns>
        Task<byte[]> GenerateEmployeeCvAsync(Employee employee);
    }
}