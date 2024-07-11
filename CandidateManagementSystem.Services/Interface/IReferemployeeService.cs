using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IReferemployeeService
    {
        Task<IEnumerable<ReferEmployeeDto>> GetReferEmployee();
        Task<ReferEmployeeDto> GetReferEmployeeById(int id);
        Task<ReferEmployeeDto> InsertReferEmployee(ReferEmployeeDto referemployee);
        Task<bool> DeleteReferEmployee(int id);
        Task<bool> UpdateReferEmployee(ReferEmployeeDto referemployee, int id);
    }
}
