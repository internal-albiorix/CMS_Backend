using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface ICandidateRepository
    {
        Task<List<CandidateModel>> GetAllCandidates();

        Task<List<CandidateModel>> GetAppointMentsCandidate();

        Task<List<CandidateModel>> GetLastestCandidates();

        Task<List<CandidateChartModel>> GetCandidatesForChart();
        Task<CandidateModel> GetCandidateByIdAsync(int CandidateId);
        Task<IEnumerable<CandidateModel>> GetFilteredCandidateData(CandidateFilterDto filterParams);
        Task<bool> RemoveCandidateTechnologies(int candidateId);
        Task<bool> CheckEmailAlreadyExist(string email, int id);
        Task<InterviewScheduleModel> GetScheduleCandidate(int id);
    }
}
