using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IStatusRepository
    {
        Task<StatusModel> GetStatusByName(string statusName);
    }
}
