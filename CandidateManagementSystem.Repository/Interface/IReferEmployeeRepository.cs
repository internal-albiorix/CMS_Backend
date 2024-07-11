using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IReferEmployeeRepository
    {
        Task<List<ReferEmployeeModel>> GetAllReferEmployee();
        Task<ReferEmployeeModel> GetReferEmployeeByIdAsync(int ReferEmployeeId);


    }
}
