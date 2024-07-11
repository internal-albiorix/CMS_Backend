using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetStatus();
        Task<StatusDto> GetStatusById(int id);
        Task<StatusDto> InsertStatus(StatusDto Status);
        Task<bool> DeleteStatus(int id);
        Task<bool> UpdateStatus(StatusModel Status, int id);
    }
}
