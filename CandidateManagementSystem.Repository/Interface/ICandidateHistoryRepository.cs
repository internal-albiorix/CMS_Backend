using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface ICandidateHistoryRepository
    {
        Task<List<CandidateHistoryModel>> GetCandidateHistoryByCandidateId(int CandidateId);
    }
}
