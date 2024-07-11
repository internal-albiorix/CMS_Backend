using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface ICandidateHistoryService
    {
        Task<CandidateHistoryDto> InsertCandidateHistory(CandidateHistoryDto candidateHistory);

        Task<List<CandidateHistoryModel>> GetCandidateHistoryByCandidateId(int candidateid);

        Task<bool> UpdateCandidateHistory(CandidateHistoryDto candidateHistorydto, int id);
    }
}
